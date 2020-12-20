using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using MudBlazor.Services.Scroll;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazor
{
    public interface IScrollListener
    {
        ElementReference Ref { get; set; }

        event EventHandler<ScrollEventArgs> OnScroll;
    }

    public class ScrollListener : IScrollListener

    {
        private readonly IJSRuntime _js;

        public ElementReference Ref { get; set; }

        public ScrollListener(IJSRuntime js)
        {
            _js = js;
        }

        private EventHandler<ScrollEventArgs> _onScroll;

        /// <summary>
        /// Subscribe to the browser onScroll event
        /// </summary>
        public event EventHandler<ScrollEventArgs> OnScroll
        {

            add => Subscribe(value);
            remove => Unsubscribe(value);
        }


        private void Subscribe(EventHandler<ScrollEventArgs> value)
        {
           
            if (_onScroll == null)
            {
                Task.Run(async () => await Start());
            }
            _onScroll += value;
        }

        private void Unsubscribe(EventHandler<ScrollEventArgs> value)
        {
            _onScroll -= value;
            if (_onScroll == null)
            {
                Cancel().ConfigureAwait(false);
            }
        }


        /// <summary>
        /// ivoked in JS, in scroll-listener.js
        /// </summary>
        /// <param name="e">The scroll event args</param>
        [JSInvokable]
        public void RaiseOnScroll(ScrollEventArgs e)
        {

            _onScroll?.Invoke(this, e);
        }


        private async ValueTask<bool> Start() =>
            await _js
            .InvokeAsync<bool>
            ("scrollListener.listenForScroll",
                DotNetObjectReference.Create(this), 
                Ref);


        private async ValueTask Cancel()
        {
            try
            {
                await _js.InvokeVoidAsync("scrollListener.cancelListener");
            }
            catch (Exception)
            {
                /* ignore */
            }
        }
    }
}
