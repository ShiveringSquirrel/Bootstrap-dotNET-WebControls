function CreateDateTimePicker(controlDt, language, mask, linkedDt) {

    // This "special" class will be used to determine if a dtp has already been created, and shouldn't be created again, losing the range setting.
    var classToAddForCheck = "classToAddForCheckCreateDateTimePicker";

    if (controlDt && !(controlDt.hasClass(classToAddForCheck))) 
    {
        //alert("Creating single dt: " + controlDt.attr('id'));
        controlDt.datetimepicker({
            locale: language,
            format: mask
        });
        controlDt.addClass(classToAddForCheck);
    }

    if(linkedDt)
    {
        //alert("Creating linked dt: " + linkedDt.attr('id'));

        linkedDt.datetimepicker({
            locale: language,
            format: mask,
            useCurrent: false
        });
        linkedDt.addClass(classToAddForCheck);

        controlDt.on("dp.change", function (e) {
            linkedDt.data("DateTimePicker").minDate(e.date);
        });

        linkedDt.on("dp.change", function (e) {
            controlDt.data("DateTimePicker").maxDate(e.date);
        });
    }
}