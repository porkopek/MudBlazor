window.blazorHelpers = {
    scrollToFragment: (elementId) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({ behavior: 'auto', block: 'center', inline: 'start' });
        }
    },
    scrollTo: (element, left, top, behavior) => {
        element.scrollTo({ left, top, behavior });
    }
};
