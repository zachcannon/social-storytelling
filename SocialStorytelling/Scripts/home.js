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

        $('#remove-story-button').click(function () {
            var idToRemove = $('#story-id-to-remove').val().toString()
            $.ajax({
                url: "/Home/RemoveStoryFromList",
                data: {"idToRemove": idToRemove}
            });
        });
    });
});

function addStory(form) {
    $.ajax({
        url: "/Home/AddStoryToDB",
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
        url: "/Home/AddEntryToStory",
        data: {
            "text": form.text.value,
            "author": form.author.value,
            "storyId": form.storyId.value
        }
    });

    alert("Entry Added to DB");
    return true;
};