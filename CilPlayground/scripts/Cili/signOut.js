function signOut() {
    $.ajax({
        url: "/User/SignOut",
        type: "POST",
        success: function () {
            location.reload();
        }
    });
}