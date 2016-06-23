function BindEvents() {
    $(".cancelButton").click(function () {
        Cancel($(this).parent().parent());
    });
    $(".saveButton").click(function () {
        Save($(this).parent().parent());
    });

    $(".deleteButton").click(function () {
        deletingRow = $(this).parent().parent();
        $("#deleteModal").modal();
    });

    $(".createButton").click(function () {
        Save($(this).parent().parent());
    });

}

var deletingRow;

$("#deleteSubmitButton").click(function () {
    Delete();
});

var conditionStorage = new Object();

var added = false;
$("#addButton").click(
    function () {
        if ($(".selected").size() == 0 && added == false) {
            added = true;
            var editRowSample = $(".editRowSample").clone();
            editRowSample.removeClass("editRowSample");
            $("#tableHeader").after(editRowSample);
            var list = $(".roleEditSample").clone();
            list.removeClass("roleEditSample");
            var child = editRowSample.children(".Role");
            child.append(list);
            BindEvents();
        }
    });


$(".adminRow").click(
function () {
    if ($(this).hasClass("canceled")) {
        $(this).removeClass("canceled");
    }
    else
        if ($(".selected").size() == 0) {
            $("#passwordHeader").removeAttr("style");
            conditionStorage = new Object();
            conditionStorage.name = $(this).children(".Name").children().text();
            conditionStorage.mail = $(this).children(".Mail").children().text();
            conditionStorage.registrationTime = $(this).children(".RegistrationTime").children().text();
            conditionStorage.role = $(this).children(".Role").children().text();
            $(this).addClass("selected");

            var passwordSample = $(".passwordSample").clone();
            passwordSample.removeClass("passwordSample");
            var buttonColSample = $(".buttonColSample").clone();
            buttonColSample.removeClass("buttonColSample");

            $(this).append(passwordSample);
            $(this).append(buttonColSample);

            $(this).children(".Role").removeClass("col-md-2").addClass("col-md-1");
            var roleId = $(this).children(".Role").attr("data-roleId");
            $(this).children(".Role").children().remove();
            var roleEditSample = $(".roleEditSample").clone();
            roleEditSample.removeClass("roleEditSample");
            $(this).children(".Role").append(roleEditSample);
                
            $(this).children(".Role").children().children("[value =" + roleId + "]").attr("selected", "selected");

            $(this).children(".Name").children().changeElementType("textarea");
            $(this).children(".Mail").children().changeElementType("textarea");
            $(this).children(".RegistrationTime").children().changeElementType("textarea");
            BindEvents();
        }
});

function Cancel(row) {
    $("#passwordHeader").css({ display: "none" });
    if (row.hasClass("add")) {
        row.remove();
        added = false;
    }
    else {
        row.removeClass("selected");
        row.addClass("canceled");
        row.children(".adminButtonCol").remove();
        row.children(".Password").remove();
        row.children(".Role").removeClass("col-md-1").addClass("col-md-2");
        row.children(".Name").children().changeElementType("div");
        row.children(".Name").children().val(conditionStorage.name);
        row.children(".Mail").children().changeElementType("div");
        row.children(".Mail").children().val(conditionStorage.mail);
        row.children(".RegistrationTime").children().changeElementType("div");
        row.children(".RegistrationTime").children().val(conditionStorage.registrationTime);
        row.children(".Role").children().remove();
        row.children(".Role").append($(".roleSample").clone());
        row.children(".Role").children().removeClass("roleSample");
        row.children(".Role").children().text(conditionStorage.role);
    }
}

function Save(row) {
    var url = "/Admin/Update";

    var model = new Object();
    model.Id = row.attr("data-id");
    model.Name = row.children(".Name").children().val();
    model.Mail = row.children(".Mail").children().val();
    model.RegistrationTime = row.children(".RegistrationTime").children().val();
    model.RoleId = row.children(".Role").children().val();
    if (row.hasClass("add")) {
        url = "/Admin/Create";
        var password = row.children(".Password").children().val();
        if (password)
            model.password = SHA512(password);
    }
    $.ajax({
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        type: "POST",
        success: function (result) {
            if (result.success === true) {
                location.reload();
            }
            else {
                alert(result.answer);
            }
        }
    });
}

function Delete() {
    var model = new Object();
    model.Id = deletingRow.attr("data-id");
    model.Name = deletingRow.children(".Name").children().val();
    model.Mail = deletingRow.children(".Mail").children().val();
    model.RegistrationTime = deletingRow.children(".RegistrationTime").children().val();
    model.RoleId = deletingRow.children(".Role").children().val();
    $.ajax({
        url: "/Admin/Delete",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        type: "POST",
        success: function (result) {
            if (result.success === true) {
                location.reload();
            }
            else {
                alert(result.answer);
            }
        }
    });
}
