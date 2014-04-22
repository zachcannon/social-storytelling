/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStoryEntry = function (i, data) {
            var x = i % 2;
            var color = 'd';
            if (x == 1) color = 'c';
            $('#entrycontainer').append('<div style="background-color:#' + color + color + color
                + ';padding:0;margin:0;position:relative;left:-6%;width:112%;border:0;border-radius:0;">'
                + data.Text + '<br>'
                + "</div>");

            /*$('#entrycontainer').append("Id: " + data.IdNumber);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("My Story ID:" + data.StoryId);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("Text: " + data.Text);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("Author: " + data.Author);
            $('#entrycontainer').append(" ");
            $('#entrycontainer').append("Submit: " + data.SubmissionDate);
            $('#entrycontainer').append("<br>");*/
        }

        var printAStorysEntryList = function (data) {
            $.each(data, printStoryEntry)
        }


        $.ajax({
            url: "/StoryView/GetEntriesForGivenStory"
        }).done(printAStorysEntryList);
        


        var printPendingEntry = function (i, data) {
            $('#pending_container').append('<div style="border:dashed;">');
            $('#pending_container').append("<h4>Entry #"+ data.IdNumber + ':</h4>');
            $('#pending_container').append('<p>'+data.Text+'</p>');
            $('#pending_container').append('<p style="font-size:smaller;">Votes: ' + data.VotesCastForMe + '</p>');
            $('#pending_container').append("</div>");
        }

        var printPendingEntryList = function (data) {
            $.each(data, printPendingEntry);
        };

        $.ajax({
            url: "/StoryView/GetPendingEntryListForGivenStory"
        }).done(printPendingEntryList);

       

    });
});
