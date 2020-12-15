using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazor.Utilities
{
    public interface IScrollManager
    {
        Task ScrollTo(int left, int top, ScrollBehavior scrollBehavior);
        Task ScrollToFragment(string id);
        Task ScrollToTop();
    }

    public class ScrollManager :  IScrollManager
    {
        [Inject] public IJSRuntime JS { get; set; }

        public async Task ScrollToFragment(string id)
        {
            await JS.InvokeVoidAsync("blazorHelpers.scrollToFragment", id);
        }

        public async Task ScrollTo(int left, int top, ScrollBehavior scrollBehavior)
        {
            await JS.InvokeVoidAsync("blazorHelpers.scrollTo", left, top, scrollBehavior.ToString());
        }

        public async Task ScrollToTop()
        {
            await ScrollTo( 0, 0, ScrollBehavior.auto);
        }
    }

    public enum ScrollBehavior
    {
        smooth,
        auto
    }
}
