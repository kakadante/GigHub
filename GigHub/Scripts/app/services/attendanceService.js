var AttendanceService = function () {

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