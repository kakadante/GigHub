using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{



    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();


        [HttpPost]
                        //public IHttpActionResult Attend([FromBody]int gigId) 
                        //todo THE ABOVE IS WHAT WAS THERE and changed to what is here below

        public IHttpActionResult Attend(AttendanceDto dto)

        {

            var userId = User.Identity.GetUserId();

                         //var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId);
                         //if (exists)
                         //    return BadRequest("The attendance already exists.");

                         //IF YOU REFACTOR THE ABOVE TO IN-LNE VARIABLE, IT WILL BE LIKE BELOW// Uncomment to see how it looks

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}

