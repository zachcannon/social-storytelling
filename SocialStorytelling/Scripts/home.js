/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var isStoryClosed = function (input, Id) {
            if (!input)
                return 'Story Open! - <button onclick="goToStoryView('+Id+');">View</button>';
            else
                return "CLOSED";
        }

        var printStory = function (i, data) {
            var x = i % 2;
            var color = 'd';
            if (x == 1) color = 'c';
            $('#storycontainer').append('<div style="background-color:#' + color + color + color
                + ';padding:0;margin:0;position:relative;text-align:left;padding-left:1cm;left:-6%;width:112%;border:0;border-radius:0;">'
                + 'Title: ' + data.Title
                + ' - ' + isStoryClosed(data.StoryClosed, data.Id)
                + '<br>'
                + "</div>");

        };

        var printStoryBook = function (data) {
            $.each(data, printStory);
        }

        $.ajax({
            url: "/Home/GetStoryList"
        }).done(printStoryBook);
        
    });
});

function goToStoryView(data) {
    $('#storybox').val(data);
    $('#submitButton').click();
}