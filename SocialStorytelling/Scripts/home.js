/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStory = function (data) {
            $('#storycontainer').append(data.Id);
            $('#storycontainer').append(data.Title);
            $('#storycontainer').append(data.Prompt);
        };

        $.ajax({
            url: "/Home/GetTheStory"
        }).done(printStory);

    });
});