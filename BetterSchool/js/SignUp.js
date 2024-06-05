﻿setTimeout(main, 1);
function main() {
    checkboxStatusChange();
    document.addEventListener("click", function (evt) {
        var flyoutElement = document.getElementById('myMultiselect'),
            targetElement = evt.target;
        do {
            if (targetElement == flyoutElement) {

                return;
            }
            targetElement = targetElement.parentNode;
        } while (targetElement);
        toggleCheckboxArea(true);
    });
    document.addEventListener("input", removeErrors);
    for (var i = 0; i < document.getElementsByTagName("input").length - 1; i++) {
        document.getElementsByTagName("input")[i].addEventListener("mousedown", removeErrors);
    }
}
function toggleCheckboxArea(onlyHide = false) {
    var checkboxes = document.getElementById("mySelectOptions");
    var displayValue = checkboxes.style.display;
    if (displayValue != "block") {
        if (onlyHide == false) {
            checkboxes.style.display = "block";
        }
    } else {
        checkboxes.style.display = "none";
    }
}
function checkboxStatusChange() {
    var multiselect = document.getElementById("mySelectLabel");
    var multiselectOption = multiselect.getElementsByTagName('option')[0];

    var values = [];
    var checkboxes = document.getElementById("mySelectOptions");
    var checkedCheckboxes = checkboxes.querySelectorAll('input[type=checkbox]:checked');
    for (const item of checkedCheckboxes) {
        var checkboxValue = item.getAttribute('value');
        values.push(checkboxValue);
    }
    var dropdownValue = "Nothing is selected";
    if (values.length > 0) {
        dropdownValue = values.join(', ');
    }
    multiselectOption.innerText = dropdownValue;
}

function removeErrors() {
    document.getElementsByTagName("h3")[0].innerText = "";
    for (var i = 0; i < document.getElementsByTagName("input").length; i++) {
        document.getElementsByTagName("input")[i].style = "border: none;";
    }
}

