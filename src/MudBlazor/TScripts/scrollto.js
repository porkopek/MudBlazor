window.blazorHelpers = {
    scrollToFragment: (elementId) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({ behavior: 'auto', block: 'center', inline: 'start' });
        }
    },
    scrollTo: (left, top, behavior) => {
        window.scrollTo({ left, top, behavior })
    }
};