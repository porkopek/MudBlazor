window.blazorHelpers = {
    scrollToFragment: (elementId, behavior) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({ behavior, block: 'center', inline: 'start' });
        }
    },
    scrollTo: (selector, left, top, behavior) => {

        element = document.querySelector(selector) || document.documentElement;
        console.log(element);
        element.scrollTo({ left, top, behavior });
    }
};
