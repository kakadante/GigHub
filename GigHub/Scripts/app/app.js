function initGigs() {
    $(".js-toggle-attendance").click(function (e) {             /*js-toggle-attendance*/

        var button = $(e.target);
        //todo 1.    $.post("/api/attendances", button.attr("data-gig-id"))
        //?  cant work since the API action result is decorated with [FROM BODY]

        //todo .    $.post("/api/attendances", { "":button.attr("data-gig-id")})
        //?  This is the best alternative but the "" (the space) looks unprofessional
        if (button.hasClass("btn-default")) {

            $.post("/api/attendances", { gigId: button.attr("data-gig-id") }) //Making an AJAX call to our API
                //?  This is the best - gigId instead of the space. (cleaner way)
                //todo  TO WRAP THE [FROM BODY]GIGID PARAMETER - IN attendancecontroller api, LETS create another class above

                //SO WE HAVE e.target IN THE LINE ABOVE AND BELOW. Lets make a VARIABLE to store it
                .done(function () { //SUCCESS
                    button
                        .removeClass("btn-default")
                        .addClass("btn-warning")
                        .text("WILL GO");
                })
                .fail(function () { //FAIL
                    alert("GIG BOOKING FAILED");
                });
        } else {
            $.ajax({
                url: "/api/attendances/" + button.attr("data-gig-id"),
                method: "DELETE"
            })


                .done(function () {
                    button
                        .removeClass("btn-warning")
                        .addClass("btn-default")
                        .text("Going ?");
                })



                .fail(function () {
                    alert("Something Failed");
                });
        }
    });
}