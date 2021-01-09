window.scrollHelpers = {
    scrollToFragment: (elementId, behavior) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.scrollIntoView({ behavior, block: 'center', inline: 'start' });
        }
    },
    //scrolls to year in MudDatePicker
    scrollToYear: (elementId, offset) => {
        var element = document.getElementById(elementId);

        if (element) {
            element.parentNode.scrollTop = element.offsetTop - element.parentNode.offsetTop - element.scrollHeight * offset;
        }
    },

    // scrolls down or up in a select input
    //increment is 1 if moving dow and -1 if moving up
    //onEdges is a boolean. If true, it waits to reach the bottom or the top
    //of the container to scroll.   
    scrollToListItem: (elementId, increment, onEdges) => {
        let element = document.getElementById(elementId);        
        if (element) {

            //this is the scroll container
            let parent = element.parentElement;
            //reset the scroll position when close the menu
            if (increment == 0) {
                parent.scrollTop = 0;
                return;
            }

            //position of the elements relative to the screen, so we can compare
            //one with the other
            //e:element; p:parent of the element; For example:eBottom is the element bottom
            let { bottom: eBottom, height:eHeight, top:eTop } = element.getBoundingClientRect();
            let { bottom: pBottom, top: pTop} = parent.getBoundingClientRect();

            if (
                //if element reached bottom and direction is down
                ((pBottom - eBottom <= 0) && increment > 0)
                //or element reached top and direction is up
                || ((eTop - pTop <= 0) && increment < 0)
                // or scroll is not constrained to the Edges
                || !onEdges
            ) {
                parent.scrollTop += eHeight* increment;                
            }
        }
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