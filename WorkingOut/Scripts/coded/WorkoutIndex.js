$(function () {
    $(".details-link").each(function (index) {
        $(this).click(function (e) {
            e.preventDefault();
            window.location = "/Workout/Details/" + $(this).attr("this-id");
        })
    })
});