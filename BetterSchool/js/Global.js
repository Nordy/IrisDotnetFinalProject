

setTimeout(main, 1);
function main() {
    setNavBack();
    window.addEventListener('scroll', setNavBack);
    document.getElementsByClassName("navExtraButton")[0].addEventListener("mouseover", dropdownOn);
    document.getElementsByClassName("navExtraButton")[0].addEventListener("mouseleave", dropdownOff);
}

function dropdownOn() {
    console.log("on");
    document.getElementsByClassName("dropdown")[0].style = "display: block;";
}

function dropdownOff() {
    console.log("off");
    document.getElementsByClassName("dropdown")[0].style = "display: none;";
}
function setNavBack() {
    navbar = document.getElementById("nav");
    if (window.scrollY < 10) {
        navbar.style.background = 'transparent';
        navbar.style.backdropFilter = 'none';
        navbar.style.webkitBackdropFilter = 'none';
        navbar.style.mozBackdropFilter = 'none';
    } else {
        navbar.style.background = 'rgba(255, 255, 255, 0.2)'
        navbar.style.backdropFilter = 'blur(20px)';
        navbar.style.webkitBackdropFilter = 'blur(20px)';
        navbar.style.mozBackdropFilter = 'blur(20px)';
    }

}

