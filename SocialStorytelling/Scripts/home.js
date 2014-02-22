/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStory = function (i, data) {
            $('#storycontainer').append(data.Id);
            $('#storycontainer').append(data.Title);
            $('#storycontainer').append(data.Prompt);
            $('#storycontainer').append("<br>");

        };

        var printStoryBook = function (data) {
            $.each(data, printStory);
        }

        $.ajax({
            url: "/Home/GetStoryList"
        }).done(printStoryBook);

    });
});