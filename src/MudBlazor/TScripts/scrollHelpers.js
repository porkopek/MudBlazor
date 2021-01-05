window.scrollHelpers = {
    scrollToFragment: (elementId) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({ behavior: 'auto', block: 'center', inline: 'start' });
        }
    },
    scrollTo: (selector, left, top, behavior) => {

        element = document.querySelector(selector) || document.documentElement;
        console.log(element);
        element.scrollTo({ left, top, behavior });
    },
    lockScroll: (selector) => {
        let element = document.querySelector(selector);
        if (element) {
            element.classList.add('scroll-locked');
        }
    },
    unlockScroll: (selector) => {
        let element = document.querySelector(selector);
        if (element) {
            element.classList.remove('scroll-locked');
        }
    }
};