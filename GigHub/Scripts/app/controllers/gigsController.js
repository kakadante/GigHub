var GigsController = function (attendanceService) {

    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };



    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        //todo 1.    $.post("/api/attendances", button.attr("data-gig-id"))
        //?  cant work since the API action result is decorated with [FROM BODY]

        //todo .    $.post("/api/attendances", { "":button.attr("data-gig-id")})
        //?  This is the best alternative but the "" (the space) looks unprofessional


        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };


    var done = function () {
        var text = (button.text() == "WILL GO") ? "Going ?" : "WILL GO";

        button.toggleClass("btn-warning").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something Failed");
    };

    return {
        init: init
    }

}(AttendanceService);