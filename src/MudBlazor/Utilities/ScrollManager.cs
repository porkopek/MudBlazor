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

    public class ScrollManager : IScrollManager
    {
        private readonly IJSRuntime _jSRuntime;

        public ScrollManager(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task ScrollToFragment(string id)
        {
            await _jSRuntime.InvokeVoidAsync("blazorHelpers.scrollToFragment", id);
        }

        public async Task ScrollTo(int left, int top, ScrollBehavior scrollBehavior)
        {
            await _jSRuntime
                .InvokeVoidAsync("blazorHelpers.scrollTo",
                                            left,
                                            top,
                                            scrollBehavior.ToString());
        }

        public async Task ScrollToTop()
        {
            await ScrollTo(0, 0, ScrollBehavior.auto);
        }
    }

    public enum ScrollBehavior
    {
        smooth,
        auto
    }
}
