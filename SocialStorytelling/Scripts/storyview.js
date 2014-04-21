/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStoryEntry = function (i, data) {
            $('#entrycontainer').append("Id: " + data.IdNumber);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("My Story ID:" + data.StoryId);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("Text: " + data.Text);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("Author: " + data.Author);
            $('#entrycontainer').append(" ");
            $('#entrycontainer').append("Submit: " + data.SubmissionDate);
            $('#entrycontainer').append("<br>");
        }

        var printAStorysEntryList = function (data) {
            $.each(data, printStoryEntry)
        }


        $.ajax({
            url: "/StoryView/GetEntriesForGivenStory"
        }).done(printAStorysEntryList);
        

        var printPendingEntry = function (i, data) {
            $('#pendingentrycontainer').append("Id: "+ data.IdNumber);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append("My Story ID:" + data.StoryId);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append("Text: "+data.Text);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append("Author: "+data.Author);
            $('#pendingentrycontainer').append(" ");            
            $('#pendingentrycontainer').append("Submit: " + data.SubmissionDate);
            $('#pendingentrycontainer').append(" ");
            $('#pendingentrycontainer').append("Votes: " + data.VotesCastForMe);
            $('#pendingentrycontainer').append("<br>");
        }

        var printPendingEntryList = function (data) {
            $.each(data, printPendingEntry);
        };

        $.ajax({
            url: "/StoryView/GetPendingEntryListForGivenStory"
        }).done(printPendingEntryList);

       

    });
});
