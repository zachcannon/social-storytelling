/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStory = function (i, data) {
            $('#storycontainer').append("Story ID: " + data.Id);
            $('#storycontainer').append(" Title: " + data.Title);
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

        var printEntry = function (i, data) {
            $('#entrycontainer').append("Id: " + data.IdNumber);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("Text: " + data.Text);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("Author: " + data.Author);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append("Submit: " + data.SubmissionDate);
            $('#entrycontainer').append("<br>");
        }

        var printEntryList = function (data) {
            $.each(data, printEntry);
        };

        $.ajax({
            url: "/StoryView/GetEntryList"
        }).done(printEntryList);

        var printPendingEntry = function (i, data) {
            $('#pendingentrycontainer').append("Id: " + data.IdNumber);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append("My Story ID:" + data.StoryId);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append("Text: " + data.Text);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append("Author: " + data.Author);
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
            url: "/StoryView/GetPendingEntryList"
        }).done(printPendingEntryList);

        //-------------------------BUTTON ACTIONS---------------------------

        var printStoryEntry = function (i, data) {
            $('#specific-entries').append("Id: " + data.IdNumber);
            $('#specific-entries').append("  ");
            $('#specific-entries').append("My Story ID:" + data.StoryId);
            $('#specific-entries').append("  ");
            $('#specific-entries').append("Text: " + data.Text);
            $('#specific-entries').append("  ");
            $('#specific-entries').append("Author: " + data.Author);
            $('#specific-entries').append(" ");
            $('#specific-entries').append("Submit: " + data.SubmissionDate);
            $('#specific-entries').append("<br>");
        }

        var printAStorysEntryList = function (data) {
            $.each(data, printStoryEntry)
        }

        $('#choose-story-button').click(function () {
            var idOfStory = $('#choose-story-id').val().toString();

            $.ajax({
                url: "/StoryView/GetEntriesForGivenStory",
                data: { "storyId": idOfStory }
            }).done(printAStorysEntryList);
        });

    });
});