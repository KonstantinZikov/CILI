$("#signUpButton").click(
    function () {

        function Check(name, mail, password, confPassword) {
            if (name.length < 4) {
                ShowMessage("Name must be longer than 4 characters.")
                return false;
            }
            if (name.length > 16) {
                ShowMessage("Name must be less than 16 characters.")
                return false;
            }

            if (!validateEmail(mail)) {
                ShowMessage("Email doesn't match format.")
                return false;
            }

            if (password.length < 6) {
                ShowMessage("Password must be longer than 6 characters.")
                return false;
            }
            if (password.length > 32) {
                ShowMessage("Password must be less than 32 characters.")
                return false;
            }
            if (password != confPassword) {
                ShowMessage("Passwords don't match.")
                return false;
            }
            return true;
        }

        function validateEmail(email) {
            var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return reg.test(email);
        }

        function ShowMessage(message) {
            $("#signUpErrorBox").text(message);
        }

        var model = new Object();
        model.Name = $("#signUpNameInput").val();
        model.Mail = $("#signUpMailInput").val();
        var password = $("#signUpPasswordInput").val();
        var confPassword = $("#signUpConfirmPasswordInput").val();
        if (Check(model.Name, model.Mail, password, confPassword))
        {
            var hashed = SHA512(password);
            model.Password = hashed;
            $.ajax({
                url: "User/SignUp",
                data: JSON.stringify(model),
                contentType: "application/json; charset=utf-8",
                type: "POST",
                success: function (result) {
                    if (result.success == true) {
                        location.reload();
                    }
                    else {
                        $("#signUpErrorBox").text(result.answer);
                    }
                }
            });
        }
            
});


