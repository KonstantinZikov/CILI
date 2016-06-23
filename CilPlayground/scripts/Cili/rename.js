$("#renameButton")
       .click(
           function () {
               var newName = $("#renameFileName").val();
               var name = $("#fileName").text();
               if (saveAsCheck) {
                   var model = new Object();
                   model.Name = name;
                   model.Code = $("#codeBox").val();
                   model.Description = $("#descriptionBox").val();
                   $.ajax({
                       url: "/UserCodes/Rename",
                       data: "name=" + name + "&newName=" + newName,
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