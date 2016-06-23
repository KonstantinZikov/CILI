function isExampleToggle() {
    $("#isExampleButton").toggleClass("isExample");
    $("#isExampleButton").toggleClass("isNotExample");
    $("#isExampleButton").blur();
}

function saveButtonClick() {
    $("#saveAsFileName").val($("#fileName").text());
    $("#saveAsModal").modal();
}

function renameButtonClick() {
    $("#renameFileName").val($("#fileName").text());
    $("#renameModal").modal();
}

function fileButtonClick(name) {
    $.ajax({
        url: "/UserCodes/GetFile",
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

var deletingFileName = "";

function Delete() {
    $.ajax({
        url: "/UserCodes/Delete",
        data: "fileName=" + deletingFileName,
        type: "POST",
        success: function (result) {
            if (result.success === true) {
                location.replace("/UserCodes");
            } else {
                alert(result.answer);
            }
        }
    });
}

function newFile() {
    $("#fileName").text("New file");
    $("#codeBox").val("");
    $("#isExampleBox").removeAttr("checked");
}

function deleteFile(name) {
    deletingFileName = name;
    $("#deleteSubmitButton").click(Delete);
    $("#deleteModal").modal();

}