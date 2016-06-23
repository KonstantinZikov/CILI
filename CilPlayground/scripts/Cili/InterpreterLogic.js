var Running = false;
function Output(data)
{
    $("#output").val($("#output").val() + data);
}

function Finished()
{
    Running = false;
    $("#playButton").attr("src", "/Content/Images/play-button.png");
    $("#status").text("Stopped");
    $("#status").css({"color":"white"})
}

function WaitingForInput()
{
    $("#status").text("Waiting for input");
    $("#status").css({"color":"red"})
}

$("#playButton").click(
function ()
{
    if (Running === false) {
        Running = true;
        $("#playButton").attr("src", "/Content/Images/stop-button.png");
        $("#status").text("Running");
        $("#status").css({ "color": "green" });
        var code = $("#codeBox").val();
        $.ajax({
            url: "/Execute",
            data: "code=" + code,
            type: "POST",
            success: function (result) {
                switch (result.condition) {
                    case "Finished":
                        Output(result.output);
                        Finished();
                        break;
                    case "WaitingForInput":
                        Output(result.output);
                        WaitingForInput();
                        break;
                    default:
                        Output("Something goes realy wrong.\r\n");
                }
            }
        });
    }
    else {
        $.ajax({
            url: "/Stop",
            type: "POST",
            success: function (result) {
                switch (result.condition) {
                    case "Finished":
                        Output(result.output);
                        Finished();
                        break;
                    default:
                        Output("Something goes realy wrong.\r\n");
                }
            }
        });
    }
});
   

$("#inputButton").click(
function () {
    if (Running === true) {           
        var input = $("#input").val();
        Output(input + "\r\n");
        $("#input").val("");
        $.ajax({
            url: "/Continue",
            data: "input=" + input,
            type: "POST",
            success: function (result) {
                switch (result.condition) {
                    case "Running":
                        Output("Program doesn't need input:)\r\n")
                        break;
                    case "Stopped":
                        Output("Program was stopped.\r\n")
                        Finished();
                        break;
                    case "Finished":
                        Output(result.output);
                        Finished();
                        break;
                    case "WaitingForInput":
                        Output(result.output);
                        WaitingForInput();
                        break;
                    default:
                        Output("Something goes realy wrong.\r\n");
                }
            }
        });
    }
});

$("#clearButton").click(
function () {
    $("#output").val("");
});
