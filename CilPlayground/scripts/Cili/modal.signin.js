$("#signInButton").click(
    function () {
        function ShowMessage(message) {
            $("#signInErrorBox").text(message);
        }

        ShowMessage("Please wait...")
        var model = new Object();
        model.Name = $("#signInNameInput").val();
        var password = $("#signInPasswordInput").val();
        var hashed = SHA512(password);
        model.Password = hashed;
        $.ajax({
            url: "User/SignIn",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            type: "POST",
            success: function (result) {
                if (result.success == true) {
                    location.reload();
                }
                else {
                    message = "Server error:("
                    if (result != undefined)
                        message = result.answer;
                    ShowMessage(message);
                }
            }
        });          
});


