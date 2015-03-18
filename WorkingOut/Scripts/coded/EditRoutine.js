$(function () {
    $("#btnAdd").click(function (e) {
        e.preventDefault();
        var addUrl = '/Workout/AddToRoutine';
        var form = $('form');
        var formData = form.serialize();
        $.post(addUrl,formData,function(data){
            $("#routine").html(data);
            AddRemoveListener();
        }, 'html');
    });

    AddRemoveListener();

    function AddRemoveListener(){
        $(".btnRmv").each(function(index){
            $(this).click(function (e) {
                e.preventDefault();
                var item = ".workoutExercises:eq(" + index + ")";
                if ($(item).hasClass("newexercise")) {
                    var item = ".deleteExercise:eq(" + index + ")";
                    $(item).val("true");
                    var item = ".workoutExercises:eq(" + index + ")";
                    $(item).fadeOut();
                }
            });
        });
    }
});