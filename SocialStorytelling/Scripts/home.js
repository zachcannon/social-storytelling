/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStory = function (data) {
            $('#storycontainer').append(data);
        };

        $.ajax({
            url: "/Home/GetTheStory"
        }).done(printStory);

    });
});