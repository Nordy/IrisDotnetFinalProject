
setTimeout(main, 1);
function main() {
    document.addEventListener("input", removeErrors);
    for (var i = 0; i < document.getElementsByTagName("input").length - 1; i++) {
        document.getElementsByTagName("input")[i].addEventListener("mousedown", removeErrors);
    }
}

function removeErrors() {
    document.getElementsByTagName("h3")[0].innerText = "";
    for (var i = 0; i < document.getElementsByTagName("input").length; i++) {
        document.getElementsByTagName("input")[i].style = "border: none;";
    }
}

