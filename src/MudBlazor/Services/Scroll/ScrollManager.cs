using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using MudBlazor.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazor
{
    /// <summary>
    /// Inject with the AddMudBlazorScrollServices extension
    /// </summary>
    public interface IScrollManager
    {
        Task ScrollTo(ElementReference element, int left, int top, ScrollBehavior scrollBehavior);
        Task ScrollToFragment(string id, ScrollBehavior behavior);
        Task ScrollToTop(ElementReference element, ScrollBehavior scrollBehavior = ScrollBehavior.Auto);
    }

    


    public class ScrollManager : IScrollManager
    {
        private readonly IJSRuntime _jSRuntime;

        public ScrollManager(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }


        /// <summary>
        /// Scroll to an url fragment
        /// </summary>
        /// <param name="id">The id of the element that is going to be scrolled to</param>
        /// <param name="behavior">smooth or auto</param>
        /// <returns></returns>
        public async Task ScrollToFragment(string id, ScrollBehavior behavior)
        {
            await _jSRuntime
                .InvokeVoidAsync("blazorHelpers.scrollToFragment", 
                                            id,
                                            behavior.ToDescriptionString());
        }

        /// <summary>
        /// Scrolls to the coordinates of the element defined in Ref property
        /// </summary>
        /// <param name="element">the ElementReference to the DOM element</param>
        /// <param name="left">x coordinate</param>
        /// <param name="top">y coordinate</param>
        /// <param name="behavior">smooth or auto</param>
        /// <returns></returns>
        public async Task ScrollTo(ElementReference element, int left, int top, ScrollBehavior behavior)
        {
            if (element.Id == null)
                throw new ScrollManagerException("The element reference must be assigned to the element that you want to scroll to");

            await _jSRuntime
                .InvokeVoidAsync("blazorHelpers.scrollTo",
                                            element,
                                            left,
                                            top,
                                            behavior.ToDescriptionString());
        }

        /// <summary>
        /// Scrolls to the top of the element defined in Ref property
        /// </summary>
        /// <param name="element">the ElementReference to the DOM element</param>
        /// <param name="scrollBehavior">smooth or auto</param>
        /// <returns></returns>
        public async Task ScrollToTop(ElementReference element, ScrollBehavior scrollBehavior = ScrollBehavior.Auto)
        {
            await ScrollTo(element, 0, 0, scrollBehavior);
        }
    }

    /// <summary>
    /// Smooth: scrolls in a smooth fashion;
    /// Auto: is immediate
    /// </summary>
    public enum ScrollBehavior
    {
        Smooth, 
        Auto 
    }
}
