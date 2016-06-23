function fileButtonClick(name) {
    $.ajax({
        url: "/UserCodes/GetFile",
        data: "fileName=" + name,
        type: "POST",
        success: function (result) {
            if (result.success === true) {
                $("#codeBox").val(result.answer.Code);
                $("#descriptionBox").text(result.answer.Description);
            }
            else {
                alert(result.answer);
            }
        }
    });
}