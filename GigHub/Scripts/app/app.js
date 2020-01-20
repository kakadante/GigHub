var GigsController = function () {

    var button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance) {             /*js-toggle-attendance*/

            
        });
    };



    var toggleAttendance = function (e) {
        button = $(e.target);
        //todo 1.    $.post("/api/attendances", button.attr("data-gig-id"))
        //?  cant work since the API action result is decorated with [FROM BODY]

        //todo .    $.post("/api/attendances", { "":button.attr("data-gig-id")})
        //?  This is the best alternative but the "" (the space) looks unprofessional
        if (button.hasClass("btn-default"))
            createAttendance();
        else
            deleteAttendance();
    };


    var createAttendance = function () {
        $.post("/api/attendances", { gigId: button.attr("data-gig-id") }) //Making an AJAX call to our API
            //?  This is the best - gigId instead of the space. (cleaner way)
            //todo  TO WRAP THE [FROM BODY]GIGID PARAMETER - IN attendancecontroller api, LETS create another class above

            //SO WE HAVE e.target IN THE LINE ABOVE AND BELOW. Lets make a VARIABLE to store it
            .done(done)
            .fail(fail);
    };


    var deleteAttendance = function () {
        $.ajax({
            url: "/api/attendances/" + button.attr("data-gig-id"),
            method: "DELETE"
        })


            .done(done)
            .fail(fail);
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

}();