
$(document).delegate('#code', 'keydown', function (e) {
    var keyCode = e.keyCode || e.which;

    if (keyCode == 9) {
        e.preventDefault();
        var start = $(this).get(0).selectionStart;
        var end = $(this).get(0).selectionEnd;

        $(this).val($(this).val().substring(0, start)
                    + "    "
                    + $(this).val().substring(end));

        $(this).get(0).selectionStart =
        $(this).get(0).selectionEnd = start + 4;
    }
});

$(document).delegate('#code','input', function () {
    $(this).val($(this).val().replace(/\t/g, "    "));
});
