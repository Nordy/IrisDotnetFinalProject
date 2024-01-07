window.addEventListener('scroll', setNavBack);
window.addEventListener('mouseover', setNavLogoHover);
window.addEventListener('mouseout', setNavLogoHover);
window.matchMedia('(prefers-color-scheme: dark)').addListener((event) => {
    if (event.matches) 
        setDarkMode();
    else 
        setWhiteMode();
});

setTimeout(main, 1);
function main() {
    setNavBack();
    setNavLogo();
    if (window.matchMedia('(prefers-color-scheme: dark)').matches)
        setDarkMode();
    else
        setWhiteMode();

}

function setNavBack() {
    var navbar = document.getElementsByClassName('nav')[0]
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

function setNavLogo() {
    var array = Array.from(document.getElementsByClassName("navImg"));
    var theme = (window.matchMedia('(prefers-color-scheme: dark)').matches) ? "white" : "dark";
    array.forEach((element) => {
        if (element.classList.contains("navSelected")) {
            element.src = `/images/${element.name}-selected-${theme}.png`;
        } else {
            element.src = `/images/${element.name}.png`;
        }
    });
}

function setNavLogoHover(event) {
    var theme = (window.matchMedia('(prefers-color-scheme: dark)').matches) ? "white" : "dark";
    if (event.target.classList.contains("navButton") || event.target.classList.contains("navImg")) {
        var image = (event.target.classList.contains("navButton")) ? event.target.getElementsByTagName('img')[0] : event.target;
        if (event.target.classList.contains("navSelected")) {
            image.src = (event.type == "mouseover") ? `/images/${event.target.name}-selected-hover.png` : `/images/${event.target.name}-selected-${theme}.png`;
        } else {
            image.src = (event.type == "mouseover") ? `/images/${event.target.name}-hover.png` : `/images/${event.target.name}-${theme}.png`;
        }
    } 
}

function setWhiteMode() {
    console.log("white");
    setNavLogo();
    var r = document.querySelector(':root');
    r.style.setProperty("--text-color", "rgb(52, 52, 52)");
    r.style.setProperty("--background-color", "rgb(223, 223, 223)");
}

function setDarkMode() {
    console.log("dark");
    setNavLogo();
    var r = document.querySelector(':root');
    r.style.setProperty("--background-color", "rgb(52, 52, 52)");
    r.style.setProperty("--text-color", "rgb(223, 223, 223)");
}