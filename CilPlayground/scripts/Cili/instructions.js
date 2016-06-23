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

    $(".supportedButton").click(function () {
        ChangeSupported($(this).parent().parent());
    });
}

var deletingRow;

$("#deleteSubmitButton").click(function () {
    Delete();
});

var conditionStorage = new Object();
var nameStorage;
var descriptionStorage;

var added = false;
$("#addButton").click(
    function () {
        if ($(".selected").length == 0) {
            if (added === false) {
                added = true;
                var instructionRowSample = $(".instructionRowSample").clone();
                instructionRowSample.removeClass("instructionRowSample");
                instructionRowSample.addClass("selected");
                $("#tableHeader").after(instructionRowSample);
                BindEvents();
            }
        }
    });

$(".instructionRow").click(
    function() {
        if ($(this).hasClass("canceled")) {
            $(this).removeClass("canceled");
        } else {
            if ($(".selected").length == 0) {
            conditionStorage.name = $(this).children(".Name").children().val();
            conditionStorage.description = $(this).children(".Description").children().val();
            conditionStorage.supported = $(this).hasClass("supported");
            $(this).addClass("selected");

            var buttonColSample = $(".buttonColSample").clone();
            buttonColSample.removeClass("buttonColSample");
            $(this).append(buttonColSample);

            $(this).children(".Description").removeClass("col-md-9").addClass("col-md-8");
            $(this).children(".Name").children().changeElementType("textarea");
            $(this).children(".Description").children().changeElementType("textarea");
            BindEvents();
        }
    }
});

function Cancel(row) {
    if (row.hasClass("add")) {
        row.remove();
        added = false;
    }
    else {
        row.removeClass("selected");
        row.addClass("canceled");
        row.children(".instructionButtonCol").remove();
        row.children(".Name").css({ padding: "" });
        row.children(".Description").css({ padding: "" }).removeClass("col-md-8").addClass("col-md-9");
        row.children(".Name").children().changeElementType("div");
        row.children(".Name").children().val(nameStorage);
        row.children(".Description").children().changeElementType("div");
        row.children(".Description").children().val(descriptionStorage);
        if (conditionStorage.supported) {
            row.addClass("supported");
            row.removeClass("unsupported");
        }
        else {
            row.addClass("unsupported");
            row.removeClass("supproted");
        }
    }
}

function ChangeSupported(row) {
    row.toggleClass("supported");
    row.toggleClass("unsupported");
}

function Save(row) {
    var url = "/Instructions/Update";
    if (row.hasClass("add"))
        url = "/Instructions/Create";
    var model = new Object();
    model.Id = row.attr("data-id");
    model.Name = row.children(".Name").children().val();
    model.Description = row.children(".Description").children().val();
    model.IsSupported = row.hasClass("supported");
    $.ajax({
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        type: "POST",
        success: function (result) {
            if (result.success == true) {
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
    model.Description = deletingRow.children(".Description").children().val();
    model.IsSupported = deletingRow.hasClass("supported");
    $.ajax({
        url: "/Instructions/Delete",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        type: "POST",
        success: function (result) {
            if (result.success == true) {
                location.reload();
            }
            else {
                alert(result.answer);
            }
        }
    });
}