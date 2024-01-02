window.addEventListener('scroll', setNavBack);
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

