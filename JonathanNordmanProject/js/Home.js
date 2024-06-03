
setTimeout(main, 1);
function main() {
    setCardHeight();
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