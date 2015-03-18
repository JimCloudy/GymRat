$(function () {
    $("#btnAdd").click(function (e) {
        e.preventDefault();
        var addUrl = '/Workout/AddToRoutine';
        $.get(addUrl, function (data) {
            var newitem = $(".workoutExercise").length;
            var html = $(data);
            html.find(".form-control").each(function (index) {
                var newId = "Routine_" + newitem + "__" + $(this).attr("id");
                var newName = "Routine[" + newitem + "]." + $(this).attr("name");
                $(this).attr("id", newId);
                $(this).attr("name", newName);
            });
            html.find(".control-label").each(function (index) {
                var newFor = "Routine_" + newitem + "__" + $(this).attr("for");
                $(this).attr("for", newFor);
            });
            html.find(".text-danger").each(function (index) {
                var newFor = "Routine[" + newitem + "]." + $(this).attr("data-valmsg-for");
                $(this).attr("data-valmsg-for", newFor);
            });
            $("#routine").append(html);
            AddRemoveListener();
        }, 'html');
    });

    AddRemoveListener();

    function AddRemoveListener(){
        $(".rmvExercise").each(function (index) {
            $(this).click(function (e) {
                e.preventDefault();
                removeNestedForm(this, "div.workoutExercise", "input.mark-for-delete");
            });
        });
    }

    function removeNestedForm(element, container, deleteElement) {
        $container = $(element).parents(container);
        $container.find(deleteElement).val('true');
        $container.fadeOut();
        return false;
    }
});