$(function () {
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    $(".dayInfo").each(function (index) {
        $(this).click(function (e) {
            if ($(this).find("p").length > 0) {
                var day = $(this).prev(".dayNum").text().trim();
                var date = months[$("#dateMonth").val()] + " " + day + ", " + $("#dateYear").val();
                $("#pickworkoutdate").text(date);
                var curDay = ($("#dateMonth").val() * 1 + 1) + "/" + day + "/" + $("#dateYear").val();
                var workoutlist = calendarEntries[curDay];
                $("#pickworkoutlist").children().remove();
                jQuery.each(workoutlist, function (index) {
                    var div = document.createElement("div");
                                                
                    var a = document.createElement("a");
                    $(a).attr("href", "/Workout/Edit/" + this.workoutID);

                    var workout = document.createElement("div");
                    $(workout).addClass("workoutHeader");
                    var p = document.createElement("p");
                    $(p).text("Workout # " + (index * 1 + 1) + ":");
                    $(p).addClass("workoutLabel");
                    $(workout).append(p);
                    var p = document.createElement("p");
                    $(p).text(((this.workoutLength != 0) ? (this.workoutLength + " min") : ""));
                    $(p).addClass("duration");
                    $(workout).append(p);

                    var weight = document.createElement("p");
                    var span = document.createElement("span");
                    $(span).text("Weight: ");
                    $(span).addClass("infoLabel");
                    $(weight).append(span);
                    $(weight).append(this.workoutWeight);
                        
                    var exercises = document.createElement("p");
                    var span = document.createElement("span");
                    $(span).text("Number of Exercises: ");
                    $(span).addClass("infoLabel");
                    $(exercises).append(span);
                    $(exercises).append(this.workoutExercises);

                    var groups = document.createElement("p");
                    var span = document.createElement("span");
                    $(span).text("Muscle Groups: ");
                    $(span).addClass("infoLabel");
                    $(groups).append(span);
                    $(groups).append(this.workoutMuscleGroups.toString().replace(/,/g,", "));

                    var notes = document.createElement("p");
                    var span = document.createElement("span");
                    $(span).text("Notes: ");
                    $(span).addClass("infoLabel");
                    $(notes).append(span);
                    if (this.workoutNotes) {
                        var toolong = (this.workoutNotes.length > 50);
                        var shortNotes = toolong ? (this.workoutNotes.substr(0, 50)) : this.workoutNotes;
                        shortNotes = toolong ? (shortNotes.substr(0, shortNotes.lastIndexOf(' ')) + "&hellip;") : shortNotes;
                        $(notes).append(shortNotes);
                    }

                    $(div).append(workout);
                    $(div).append(weight);
                    $(div).append(exercises);
                    $(div).append(groups);
                    $(div).append(notes);

                    var day = this.workoutID;

                    $(div).on('click', function () {
                        window.location = "/Workout/Edit/" + day;
                    });

                    $("#pickworkoutlist").append(div);
                });
                $("#workoutSelect").modal('toggle');
                $(".modal-dialog").css({
                    'margin-top': function () {
                        return $(".modal-content").outerHeight() / 2 * -1;
                    }
                });
            }
        });
    });

    $(".addToCalendar").each(function (index) {
        $(this).click(function (e) {
            var addDate = new Date($(this).attr("add-date"));
            window.location = "/Workout/Create/" + addDate.getFullYear() + "/" + (addDate.getMonth() + 1) + "/" + (addDate.getDate() < 10 ? "0" : "") + addDate.getDate();
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
                $(this).find(".dayNum p").text("");
                $(this).find(".dayAdd button").attr("add-date", "");
                return;
            }
            dayOfMonth++;
            if (dayOfMonth <= daysInMonth) {
                var curDay = (newDate.getMonth() + 1) + "/" + dayOfMonth + "/" + newDate.getFullYear();
                $(this).addClass("dayActive");
                $(this).find(".dayNum p").text(dayOfMonth);
                $(this).find(".dayAdd button").attr("add-date", curDay);
                var workouts = calendarEntries[curDay];
                if (workouts != null) {
                    var p = document.createElement("p");
                    $(p).text("You have " + workouts.length + (workouts.length == 1 ? " workout" : " workouts")  + " on this day.");
                    $(this).find(".dayInfo").append(p);
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
        highlightCurrentDate();
    }

    function highlightCurrentDate() {
        var month = $("#dateMonth").val();
        var year = $("#dateYear").val();
        var currentDate = new Date();

        $(".dayNum").removeClass("highlight");

        if (currentDate.getFullYear() == year && currentDate.getMonth() == month) {
            var dayNum = $(".dayActive .dayNum");
            dayNum.each(function (index) {
                if ($(this).text() == currentDate.getDate()) {
                    $(this).addClass("highlight");
                }
            });
        }
    }

    $("#workoutSelect").modal({
        backdrop: false,
        show: false
    });

    highlightCurrentDate();
});