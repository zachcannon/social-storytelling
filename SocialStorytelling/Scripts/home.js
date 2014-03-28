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

        $('#remove-pending-entry-button').click(function () {
            var idToRemove = $('#pending-entry-id-to-remove').val().toString();

            $.ajax({
                url: "/Home/RemovePendingEntry",
                data: { "idToRemove": idToRemove }
            });
        });

        $('#promote-pending-entry-button').click(function () {
            var idToPromote = $('#pending-entry-id-to-promote').val().toString();

            $.ajax({
                url: "/Home/PromotePendingEntry",
                data: { "idToPromote": idToPromote }
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

function addPendingEntry(form) {
    $.ajax({
        url: "/Home/AddNewPendingEntry",
        data: {
            "text": form.text.value,
            "author": form.author.value,
            "storyId": form.storyId.value
        }
    });

    alert("Pending Entry Added to DB");
    return true;
};

function voteForPendingEntry(form) {
    $.ajax({
        url: "/Home/VoteForPendingEntry",
        data: {
            "pendingEntryId": form.pendingEntryId.value,
            "username": form.username.value,
            "password": form.password.value
        }
    });

    return false;
}

function registerUser(form) {
    var updateRegisterReturn = function (data) {
        $('#registerReturn').append(data);
    };

    $.ajax({
        url: "/Home/RegisterNewUser",
        type: 'POST',
        data: {
            "username": form.username.value,
            "password": form.password.value
        }        
    }).done(updateRegisterReturn);

    return false;
};

function loginUser(form) {
    var updateLoginReturn = function (data) {
        $('#loggedInUser').text('');
        $('#loggedInUser').append(data);
    };

    $.ajax({
        url: "/Home/LoginUser",
        type: 'POST',
        data: {
            "username": form.username.value,
            "password": form.password.value
        }
    }).done(updateLoginReturn);

    return false;
};

