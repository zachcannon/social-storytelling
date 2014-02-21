/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {

    $(function () {
        $.ajax({
            url: "/Home/RetrieveStory"
        }).done(updatePlayersResources);
    });

});


var updatePlayersResources = function (storyText) {
   
};