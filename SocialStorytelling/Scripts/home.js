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

        $('#remove-story-button').click(function () {
            var idToRemove = $('#story-id-to-remove').val().toString();

            $.ajax({
                url: "/Home/RemoveStory",
                data: {"idToRemove": idToRemove}
            });
        });

        $('#remove-entry-button').click(function () {
            var idToRemove = $('#entry-id-to-remove').val().toString();

            $.ajax({
                url: "/Home/RemoveEntry",
                data: { "idToRemove": idToRemove }
            });
        });

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

function addStory(form) {
    $.ajax({
        url: "/Home/AddNewStory",
        data: {
            "title": form.title.value,
            "prompt": form.prompt.value
        }
    });

    alert("Story Added to DB");
    return true;
};

function addEntry(form) {
    $.ajax({
        url: "/Home/AddNewEntry",
        data: {
            "text": form.text.value,
            "author": form.author.value,
            "storyId": form.storyId.value
        }
    });

    alert("Entry Added to DB");
    return true;
};

