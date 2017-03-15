var continentsCheckboxes;
var selectAllContinentsBtn = $('#select-all-btn');
function noticeMeSenpai() {
    // I can't believe it, but this actually does not work with the for loop. Wow.
    var delay = 50;
    continentsCheckboxes[0].focus();
    setTimeout(function () { continentsCheckboxes[1].focus() }, delay);
    setTimeout(function () { continentsCheckboxes[2].focus() }, delay * 2);
    setTimeout(function () { continentsCheckboxes[3].focus() }, delay * 3);
    setTimeout(function () { continentsCheckboxes[4].focus() }, delay * 4);
    setTimeout(function () { continentsCheckboxes[5].focus() }, delay * 5);
};

function areAllCheckboxesOn(checkboxes) {
    var countChecked = 0;
    checkboxes.each(function () {
        if ($(this).is(':checked'))
            countChecked++;
    });

    return countChecked == checkboxes.length;
}
function setAllNoneBtnText(button, checkboxes, buttonIcon) {
    if (buttonIcon) {
        if (areAllCheckboxesOn(checkboxes))
            button.find('img').attr('src', '/img/deselect-all.svg');
        else
            button.find('img').attr('src', '/img/select-all.svg');
    }
    else {
        if (areAllCheckboxesOn(checkboxes))
            button.text('Deselect all');
        else
            button.text('Select all');
    }
}

function setupSelectAllNoneCheckboxes(button, checkbxes) {
    checkbxes.each(function () {
        $(this).parent().click(function () {
            setAllNoneBtnText(button, checkbxes);
        });
    });

    button.click(function () {
        var allOn = areAllCheckboxesOn(checkbxes);
        checkbxes.each(function () {
            if (allOn)
                $(this).prop('checked', false);
            else
                $(this).prop('checked', true);
        });
        setAllNoneBtnText(button, checkbxes);
    });
}

$(document).ready(function () {
    continentsCheckboxes = $('#continents .checkbox label input');
    continentsCheckboxes.each(function () {
        $(this).parent().click(function () {
            setAllNoneBtnText(selectAllContinentsBtn, continentsCheckboxes, true);
        });
    });

    selectAllContinentsBtn.click(function () {
        var allOn = areAllCheckboxesOn(continentsCheckboxes);
        continentsCheckboxes.each(function () {
            if (allOn)
                $(this).prop('checked', false);
            else
                $(this).prop('checked', true);
        });
        setAllNoneBtnText(selectAllContinentsBtn, continentsCheckboxes, true);
    });

    setAllNoneBtnText(selectAllContinentsBtn, continentsCheckboxes, true);
});