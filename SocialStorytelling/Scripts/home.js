/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {

        var printStory = function (i, data) {
            $('#storycontainer').append(data.Id);
            $('#entrycontainer').append("  ");
            $('#storycontainer').append(data.Title);
            $('#entrycontainer').append("  ");
            $('#storycontainer').append(data.Prompt);
            $('#storycontainer').append("<br>");

        };

        var printStoryBook = function (data) {
            $.each(data, printStory);
        }

        $.ajax({
            url: "/Home/GetStoryList"
        }).done(printStoryBook);

        var printEntry = function (i, data) {
            $('#entrycontainer').append(data.IdNumber);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append(data.Text);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append(data.Author);
            $('#entrycontainer').append("  ");
            $('#entrycontainer').append(data.SubmissionDate);
            $('#entrycontainer').append("<br>");
        }

        var printEntryList = function (data) {
            $.each(data, printEntry);
        };

        $.ajax({
            url: "/Home/GetEntryList"
        }).done(printEntryList);

        var printPendingEntry = function (i, data) {
            $('#pendingentrycontainer').append(data.IdNumber);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append(data.Text);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append(data.Author);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append(data.StoryId);
            $('#pendingentrycontainer').append("  ");
            $('#pendingentrycontainer').append(data.SubmissionDate);
            $('#pendingentrycontainer').append("<br>");
        }

        var printPendingEntryList = function (data) {
            $.each(data, printPendingEntry);
        };

        $.ajax({
            url: "/Home/GetPendingEntryList"
        }).done(printPendingEntryList);

        //-------------------------BUTTON ACTIONS---------------------------

        var printStoryEntry = function (i, data) {
            $('#specific-entries').append(data.IdNumber);
            $('#specific-entries').append("  ");
            $('#specific-entries').append(data.Text);
            $('#specific-entries').append("  ");
            $('#specific-entries').append(data.Author);
            $('#specific-entries').append("  ");
            $('#specific-entries').append(data.SubmissionDate);
            $('#specific-entries').append("<br>");
        }

        var printAStorysEntryList = function (data) {
            $.each(data, printStoryEntry)
        }

        $('#choose-story-button').click(function () {
            var idOfStory = $('#choose-story-id').val().toString();

            $.ajax({
                url: "/Home/GetEntriesForGivenStory",
                data: { "storyId": idOfStory }
            }).done(printAStorysEntryList);
        });

    });
});
