var attendanceService = function () {

    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId }) //Making an AJAX call to our API
            //?  This is the best - gigId instead of the space. (cleaner way)
            //todo  TO WRAP THE [FROM BODY]GIGID PARAMETER - IN attendancecontroller api, LETS create another class above

            //SO WE HAVE e.target IN THE LINE ABOVE AND BELOW. Lets make a VARIABLE to store it
            .done(done)
            .fail(fail);
    };


    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        })


            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();








var GigsController = function (attendanceService) {

    var button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);             /*js-toggle-attendance*/
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