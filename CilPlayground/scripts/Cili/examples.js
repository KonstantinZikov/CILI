function fileButtonClick(name) {
    $.ajax({
        url: "/Examples/GetFile",
        data: "fileName=" + name,
        type: "POST",
        success: function (result) {
            if (result.success === true) {
                $("#fileName").text(result.answer.Name);
                $("#codeBox").val(result.answer.Code);
                $("#descriptionBox").val(result.answer.Description);
                if (result.answer.IsExample) {
                    $("#isExampleButton").addClass("isExample");
                    $("#isExampleButton").removeClass("isNotExample");
                } else {
                    $("#isExampleButton").addClass("isNotExample");
                    $("#isExampleButton").removeClass("isExample");
                }

            }
            else {
                alert(result.answer);
            }
        }
    });
}