
window.matchMedia('(prefers-color-scheme: dark)').addListener((event) => {
    if (event.matches)
        setDarkMode();
    else
        setWhiteMode();
});
setTimeout(main, 1);
function main() {
    setCardHeight();
    if (window.matchMedia('(prefers-color-scheme: dark)').matches)
        setDarkMode();
    else
        setWhiteMode();


}


function setWhiteMode() {
}

function setDarkMode() {

}

window.addEventListener('resize', setCardHeight);

function setCardHeight() {
    var array = Array.from(document.getElementsByClassName("det"))
    array.forEach((item) => {
        item.style.height = ``;
    });
    var height = document.getElementsByClassName("det")[0].offsetHeight;
    array.forEach((item) => {
        item.style.height = `${height}px`;
    });

}