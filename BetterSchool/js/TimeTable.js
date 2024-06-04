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