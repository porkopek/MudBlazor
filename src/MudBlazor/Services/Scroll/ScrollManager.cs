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
        Task ScrollTo(int left, int top, ScrollBehavior scrollBehavior);
        Task ScrollToFragment(string id);
        Task ScrollToTop(ScrollBehavior scrollBehavior= ScrollBehavior.Auto);
        ElementReference Ref { get; set; }
    }


    public class ScrollManager : IScrollManager
    {
        private readonly IJSRuntime _jSRuntime;

        public ScrollManager(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        /// <summary>
        /// The element reference that NEEDS to be attached to the @ref
        /// of the element that will receive scroll events
        /// </summary>
        public ElementReference Ref { get; set; }

        /// <summary>
        /// Scroll to an url fragment
        /// </summary>
        /// <param name="id">The id of the element that is going to be scrolled to</param>
        /// <returns></returns>
        public async Task ScrollToFragment(string id)
        {
            await _jSRuntime.InvokeVoidAsync("blazorHelpers.scrollToFragment", id);
        }

        /// <summary>
        /// Scrolls to the coordinates of the element defined in Ref property
        /// </summary>
        /// <param name="left">x coordinate</param>
        /// <param name="top">y coordinate</param>
        /// <param name="scrollBehavior">smooth or auto</param>
        /// <returns></returns>
        public async Task ScrollTo(int left, int top, ScrollBehavior scrollBehavior)
        {
            if (Ref.Id == null) 
                throw new ScrollManagerException("The Ref property must be assigned to the @ref of the element you want to scroll to");

            await _jSRuntime
                .InvokeVoidAsync("blazorHelpers.scrollTo",
                                            Ref,
                                            left,
                                            top,
                                            scrollBehavior.ToDescriptionString());
        }

        /// <summary>
        /// Scrolls to the top of the element defined in Ref property
        /// </summary>
        /// <param name="scrollBehavior">smooth or auto</param>
        /// <returns></returns>
        public async Task ScrollToTop(ScrollBehavior scrollBehavior=ScrollBehavior.Auto)
        {
            await ScrollTo(0, 0, scrollBehavior);
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
