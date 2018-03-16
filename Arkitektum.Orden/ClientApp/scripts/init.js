function findAncestor(el, cls) {
    while ((el = el.parentElement) && !el.classList.contains(cls));
    return el;
}

function initDropdownElements() {
    let overlayElementsIsDefined = document.getElementsByClassName('dropdown-box-toggle') !== undefined;
    if (overlayElementsIsDefined) {
        let overlayElements = document.getElementsByClassName('dropdown-box-toggle');
        for (var i = 0; i < overlayElements.length; i++) {
            let overlayElement = overlayElements[i];

            document.onclick = function (event) {
                for (var i = 0; i < overlayElements.length; i++) {
                    overlayElements[i].classList.remove('active');
                }
                var thisNodeClassList = event.target.classList !== undefined ? event.target.classList : false;
                var parentNodeClassList = event.target.parentNode !== null && event.target.parentNode.classList !== undefined ? event.target.parentNode.classList : false;

                if (event.target.classList.contains('dropdown-box-toggle')) {
                    event.target.classList.add('active');
                } else {
                    let ancestorElement = findAncestor(event.target, 'dropdown-box-toggle');
                    if (ancestorElement !== null) {
                        ancestorElement.classList.add('active');
                    }
                }
            }
        }
    }
}

function initFunction() {
    initDropdownElements();
}

document.addEventListener('DOMContentLoaded', initFunction, false);
