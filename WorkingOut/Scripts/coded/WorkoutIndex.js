$(function () {
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    $(".dayInfo").each(function (index) {
        $(this).click(function (e) {
            if ($(this).find(".singleID").length > 0) {
                window.location = "/Workout/Edit/" + $(this).find(".singleID:first").val();
            }

        });
    });

    $(".addToCalendar").each(function (index) {
        $(this).click(function (e) {
            window.location = "/Workout/Create/?addDate=" + $(this).attr("add-date");
        });
    });

    $("#date_div h2").click(function (index) {
        if (!$("#date_input").visible) {
            $("#date_info").hide();
            $("#date_input").show();
        }
    });

    $("#cancel").click(function (index) {
        $("#date_input").hide();
        $("#date_info").show();
        $("#select_month option").each(function (index) {
            if ($(this).val() == $("#dateMonth").val()) {
                $(this).attr("selected", true);
            }
            else {
                $(this).attr("selected", false);
            }
        });

        $("#select_year option").each(function (index) {
            if ($(this).val() == $("#dateYear").val()) {
                $(this).attr("selected", true);
            }
            else {
                $(this).attr("selected", false);
            }
        });
    });

    $("#go").click(function (index) {
        $("#dateMonth").val($("#select_month").val());
        $("#dateYear").val($("#select_year").val());
        changeDate();
    });

    $("#now").click(function (index) {
        var now = new Date();
        $("#dateMonth").val(now.getMonth());
        $("#dateYear").val(now.getFullYear());
        changeDate();
    });

    $(".glyphicon-chevron-left").click(function (index) {
        changeDate("prev");
    });

    $(".glyphicon-chevron-right").click(function (index) {
        changeDate("next");
    });

    function changeDate(direction) {
        var newMonth = $("#dateMonth").val();
        var newYear = $("#dateYear").val();
        var newDate = new Date(newYear, newMonth);
        if (direction == "next") {
            newMonth++;
        }
        if (direction == "prev") {
            newMonth--;
        }
        newDate.setMonth(newMonth);
        $("#dateMonth").val(newDate.getMonth());
        $("#dateYear").val(newDate.getFullYear());
        $(".darkHeadingDiv, table").fadeOut();
        $(".darkHeadingDiv, table").promise().done(function () {
            drawCalendar(newDate);
            $("#date_input").hide();
            $("#date_info").show();
            $(".darkHeadingDiv, table").fadeIn();
        });
    }

    function drawCalendar(newDate) {
        $("#date_info h2").text(months[newDate.getMonth()] + " " + newDate.getFullYear());
        var dayCells = $("tbody td");
        var dayOfMonth = 0;
        var daysInMonth = new Date(newDate.getFullYear(), newDate.getMonth() + 1, 0).getDate();
        var startDay = new Date(newDate.getFullYear(), newDate.getMonth(), 1).getDay();
        dayCells.each(function (index) {
            $(this).removeClass();
            $(this).find(".dayInfo").children().remove();
            if (index < startDay) {
                $(this).addClass("dayNotActive");
                $(this).find(".dayNum").text("");
                $(this).find(".dayAdd button").attr("add-date", "");
                return;
            }
            dayOfMonth++;
            if (dayOfMonth <= daysInMonth) {
                var curDay = (newDate.getMonth() + 1) + "/" + dayOfMonth + "/" + newDate.getFullYear();
                $(this).addClass("dayActive");
                $(this).find(".dayNum").text(dayOfMonth);
                $(this).find(".dayAdd button").attr("add-date", curDay);
                var workouts = calendarEntries[curDay];
                if (workouts != null) {
                    var p = document.createElement("p");
                    $(p).text("You have " + workouts.length + " workouts on this day.");
                    $(this).find(".dayInfo").append(p);
                    if (workouts.length == 1) {
                        var hidden = document.createElement("input");
                        $(hidden).attr("type", "hidden");
                        $(hidden).addClass("singleID");
                        $(hidden).val(workouts[0].workoutID);
                        $(this).find(".dayInfo").append(hidden);
                    }
                }
            }
            else {
                $(this).addClass("dayNotActive");
                $(this).find(".dayNum").text("");
                $(this).find(".dayAdd button").attr("add-date", "");
            }
        });
        var weeks = $("tbody tr");
        weeks.each(function (index) {
            if ($(this).find(".dayActive").length > 0) {
                $(this).removeClass();
                $(this).addClass("weekShow");
            }
            else {
                $(this).removeClass();
                $(this).addClass("weekHidden");
            }
        });

        $("#select_month option").each(function (index) {
            if ($(this).val() == newDate.getMonth()) {
                $(this).attr("selected", true);
            }
            else {
                $(this).attr("selected", false);
            }
        });

        $("#select_year option").each(function (index) {
            if ($(this).val() == newDate.getFullYear()) {
                $(this).attr("selected", true);
            }
            else {
                $(this).attr("selected", false);
            }
        });
    }
});