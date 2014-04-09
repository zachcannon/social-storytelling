/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStory = function (i, data) {
            $('#storycontainer').append("Story ID: "+data.Id);
            $('#storycontainer').append(" Title: "+data.Title);
            $('#storycontainer').append(" Prompt: " + data.Prompt);
            $('#storycontainer').append(" Closed? : " + data.StoryClosed);
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
