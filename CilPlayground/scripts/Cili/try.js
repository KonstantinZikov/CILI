function NextExample() {
    $.ajax({
        url: "/Try/NextExample",
        data: "index="+exampleNumber,
        type: "POST",
        success: function (result) {
            if (result.success === true) {
                $("#codeBox").val(result.document.Code);
                $("#descriptionBox").text(result.document.Description);
                exampleNumber++;
            }
            else {
                alert(result.answer);
            }
        }
    });
}

var exampleNumber = 0;