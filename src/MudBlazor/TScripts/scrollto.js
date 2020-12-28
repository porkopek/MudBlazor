window.blazorHelpers = {
    scrollToFragment: (elementId, behavior) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({ behavior, block: 'center', inline: 'start' });
        }
    },
    scrollTo: (element, left, top, behavior) => {
        element.scrollTo({ left, top, behavior });
    }
};
