window.scrollListener = {
  throttleScrollHandlerId: -1,
  dotnet: undefined,

  listenForScroll: function (dotnetRef, element) {
    this.element = element;
    this.dotnet = dotnetRef;
    this.element.addEventListener(
      "scroll",
      this.throttleScrollHandler.bind(this),
      false
    );

    this.scrollHandler();
  },

  throttleScrollHandler: function () {
    clearTimeout(this.throttleScrollHandlerId);

    this.throttleScrollHandlerId = window.setTimeout(
      this.scrollHandler.bind(this),
      300
    );
  },

  scrollHandler: function () {
      try {
          console.log("x: " + window.scrollX, "y: " + window.scrollY)
      this.dotnet.invokeMethodAsync("RaiseOnScroll", {
        x: window.scrollX,
        y: window.scrollY,
      });
    } catch (error) {
      console.log("[MudBlazor] Error in scrollHandler:", { error });
    }
  },

  cancelListener: function () {
    //console.log("[MudBlazor] cancelListener");
    this.element.removeEventListener("scroll", this.throttleScrollHandler);
  },
};
