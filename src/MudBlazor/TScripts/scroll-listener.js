window.scrollListener = {
    throttleScrollHandlerId: -1,

    // throttled event listener
    listenForScroll: function (dotnetReference, selector) {
        //if selector is null, attach to document
        let element = selector ? document.querySelector(selector) : document;

        // add the event listener
        element.addEventListener(
            'scroll',
            this.throttleScrollHandler.bind(this, dotnetReference),
            false
        );
    },

    // fire the event just once each 100 ms, hardcoded
    throttleScrollHandler: function (dotnetReference, event) {
        clearTimeout(this.throttleScrollHandlerId);

        this.throttleScrollHandlerId = window.setTimeout(
            this.scrollHandler.bind(this, dotnetReference, event),
            100
        );
    },

    // when scroll event is fired, pass this information to
    // the RaiseOnScroll C# method of the ScrollListener
    scrollHandler: function (dotnetReference, event) {
        try {
            let element = event.target;
            let child = element.firstElementChild;
            let boundingClientRect = element.getBoundingClientRect();
            let firstChildBoundingClientRect = child.getBoundingClientRect();
            let scrollTop = element.scrollTop;
            let scrollHeight = element.scrollHeight;
            let scrollWidth = element.scrollWidth;
            let scrollLeft = element.scrollLeft;

            dotnetReference.invokeMethodAsync('RaiseOnScroll', {
                boundingClientRect,
                firstChildBoundingClientRect,
                scrollLeft,
                scrollTop,
                scrollHeight,
                scrollWidth,
            });
        } catch (error) {
            console.log('[MudBlazor] Error in scrollHandler:', { error });
        }
    },

    //remove event listener
    cancelListener: function (selector) {
        let element = selector ? document.querySelector(selector) : document;

        element.removeEventListener('scroll', this.throttleScrollHandler);
    },
};
