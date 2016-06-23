$("#saveAsButton")
        .click(
            function () {
                var name = $("#saveAsFileName").val();
                if (saveAsCheck) {
                    var model = new Object();
                    model.Name = name;
                    model.Code = $("#codeBox").val();
                    model.Description = $("#descriptionBox").val();
                    if ($("#isExampleButton").hasClass("isExample")) {
                        model.IsExample = true;
                    }
                    $.ajax({
                        url: "/UserCodes/Save",
                        data: JSON.stringify(model),
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        success: function (result) {
                            if (result.success === true) {
                                location.replace("/UserCodes/Document/" + name);
                            }
                            else {
                                alert(result.answer);
                            }
                        }
                    });
                }


            });

function saveAsCheck(name) {
    if (name.length === 0) {
        SaveAsShowMessage("Name can't be empty.");
        return false;
    }
    if (name.length > 32) {
        SaveAsShowMessage("Name mast be less then 32 charachters.");
        return false;
    }
    return true;
}

function SaveAsShowMessage(message) {
    $("#saveAsErrorBox").text(message);
}