/// <reference path="~/Scripts/jquery-ui-1.10.3.min.js" />

$(document).ready(function () {
    $(function () {
        var x = document.cookie;
        if (document.cookie.search("TweetAuthCookie") > -1) {
            $("#accountitem2").empty();
        } else {
            $("#accountitem1").empty();
            $("#accountitem3").empty();
        }
    });
});