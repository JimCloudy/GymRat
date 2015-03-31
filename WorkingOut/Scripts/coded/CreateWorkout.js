﻿$(function () {

    //javascript model of the routine inside the workout
    var routine = [];

    //workoutExercise(id of exercise, array of set)
    function workoutExercise(id, s) {
        this.exerciseID = id;
        this.sets = s;
    }

    //set(reps, weight)
    function set(r, w) {
        this.reps = r;
        this.weight = w;
    }
    
    populateRoutine();


    $("#addExercise").click(function (e) {
        e.preventDefault();
        var sets = [];
        $(".set_entry").each(function (s) {
            var r = $(this).find(".set_entry_rep:eq(0) .set_entry_input:eq(0)").val();
            var w = $(this).find(".set_entry_wt:eq(0) .set_entry_input:eq(0)").val();
            if (r.trim() != "" || w.trim() != "") {
                sets.push({ reps: r, weight: w });
            }            
        });

        var exerciseID = $("#ExerciseName option:selected").val();

        var lookup = "#exercise_" + exerciseID;

        if ($(lookup).length > 0) {
            var exerciseDiv = $(lookup);
            var setRows = exerciseDiv.find(".set_rows:eq(0)");
            var lastRowNum = setRows.find(".form-group").length;
            var lookup = "#" + exerciseID + "_row_" + lastRowNum;
            var row = $(lookup);
            var rowSets = row.find(".col-md-3").length;
            for (var i = 0; i < sets.length; i++) {
                if (rowSets > 3) {
                    lastRowNum++;
                    row = document.createElement("div")
                    $(row).addClass("form-group")
                    $(row).attr("id", exerciseID + "_row_" + lastRowNum);
                    $(setRows).append(row);
                    rowSets = 0;
                }
                rowSets++;
                addSet(row, exerciseID, sets[i]);
            }
        }
        else {
            //create main exercise div
            var exerciseDiv = document.createElement("div");
            $(exerciseDiv).addClass("form-group grey_div");
            $(exerciseDiv).attr("id", "exercise_" + exerciseID);
            
            //create div to hold name of exercise and edit delete links
            var exerciseName = document.createElement("div");
            $(exerciseName).addClass("col-md-2");

            //create element to hold name of exercise
            var p = document.createElement("p");
            $(p).addClass("set_added_name");
            $(p).html($("#ExerciseName option:selected").text());

            //create element to hold id for passing to server
            var routineIndex = findWorkoutExercise(exerciseID);
            var input = document.createElement("input");
            $(input).attr("type", "hidden");
            $(input).attr("name", "Routine[" + routineIndex + "].ExerciseID");
            $(input).val(exerciseID);

            $(exerciseName).append(p);
            $(exerciseName).append(input);

            $(exerciseDiv).append(exerciseName);

            //create div that holds the rows of sets
            var setRows = document.createElement("div");
            $(setRows).addClass("col-md-10 set_rows");

            //create div that is a row of sets
            var row = document.createElement("div");
            $(row).addClass("form-group");
            $(row).attr("id", exerciseID + "_row_1");

            for (var i = 0; i < sets.length; i++) {
                addSet(row, exerciseID,sets[i]);
            }

            $(setRows).append(row);
            $(exerciseDiv).append(setRows);

            $("#addedExercises").append(exerciseDiv);
        }

        clearAddExercise();
    });

    //addSet(row number, exercise id, data for set)
    function addSet(row, exerciseID, setdata) {
        var routineIndex = findWorkoutExercise(exerciseID);

        var setIndex = addSetData(routineIndex, setdata);

        var routineName = "Routine[" + routineIndex + "].";
        var routineId = "Routine_" + routineIndex + "_";
        var setName = "Sets[" + setIndex + "].";
        var setId = "_Sets_" + setIndex + "_";

        //create the container for the set
        var set = document.createElement("div");
        $(set).addClass("col-md-3");
        $(set).attr("id", exerciseID + "_set_" + setIndex);

        //create div to be styled
        var setAdded = document.createElement("div");
        $(setAdded).addClass("set_added");

        //create header to hold the set number
        var setAddedNum = document.createElement("h6");
        $(setAddedNum).addClass("set_added_num");
        $(setAddedNum).html((setIndex + 1).toString());

        //create container for the reps and weight info
        var setAddedInfo = document.createElement("div");
        $(setAddedInfo).addClass("display-inline set_added_info");

        //create text of rep and weight info
        var p = document.createElement("p");
        $(p).html(setdata.reps + " x " + setdata.weight + " lb");

        //create container for the reps and weight info
        var setAddedInput = document.createElement("div");
        $(setAddedInput).addClass("display-inline set_added_input");
        $(setAddedInput).hide();

        //create input element for set rep
        var rep = document.createElement("input");
        $(rep).attr("type", "text");
        $(rep).attr("name", routineName + setName + "Reps");
        $(rep).val(setdata.reps);

        //create input element for set weight
        var weight = document.createElement("input");
        $(weight).attr("type", "text");
        $(weight).attr("name", routineName + setName + "Weight");
        $(weight).val(setdata.weight);

        //create spans for "X" and "lb"
        var x = document.createElement("span");
        $(x).text(" x ");
        var lbs = document.createElement("span");
        $(lbs).text(" lb");

        //attach everything together
        $(setAddedInfo).append(p);
        $(setAddedInput).append(rep, x, weight, lbs);
        $(setAdded).append(setAddedNum, setAddedInfo, setAddedInput);
        $(set).append(setAdded);
        $(row).append(set);
    }

    function findWorkoutExercise(id) {
        var index = -1;
        for (var i = 0; i < routine.length; i++) {
            if (routine[i].exerciseID == id) {
                index = i;
                i = routine.length;
            }
        }
        if (index == -1) {
            index = routine.push(new workoutExercise(id, [])) - 1;
        }
        return index;
    }

    function addSetData(index, data) {
        return routine[index].sets.push(new set(data.reps, data.weight)) - 1;
    }

    function clearAddExercise() {
        $(".set_entry").each(function (s) {
            $(this).find(".set_entry_rep:eq(0) .set_entry_input:eq(0)").val("");
            $(this).find(".set_entry_wt:eq(0) .set_entry_input:eq(0)").val("");
        });
    }

    function populateRoutine() {
        $("input[name='ExerciseID']").each(function (index) {
            var thisId = $(this).val();
            var exerciseDiv = "#exercise_" + thisId;
            var sets = [];
            $(exerciseDiv).find(".set_added_input").each(function (index) {
                var thisSet = new set($(this).children("input:eq(0)").val(), $(this).children("input:eq(1)").val());
                sets.push(thisSet);
            });
            var thisExercise = new workoutExercise(thisId, sets);
            routine.push(thisExercise);
        });
    }
});