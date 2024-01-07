
window.addEventListener('resize', setCardHeight);
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

function setWhiteMode() {
    console.log("white h");
    document.getElementById("demo").src = "images/logo-dark.png";
}

function setDarkMode() {
    console.log("dark h");
    document.getElementById("demo").src = "images/logo-white.png";
}