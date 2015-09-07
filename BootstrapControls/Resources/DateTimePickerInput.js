function CreateDateTimePicker(controlDt, language, mask) {
    //if (controlDt) {
    controlDt.datetimepicker({
        locale: language,
        format: mask
    });
    //}
}

//$(document).ready(function () {
//});