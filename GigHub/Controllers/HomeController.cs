using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;                  //From Here to...
        private readonly AttendanceRepository _attendanceRepository;

        public HomeController()                    /*--This is Just a constructor (CTOR +TAB*2)--*/
        {
                _context = new ApplicationDbContext();          //HERE ->   All this is retrieving data from database
                _attendanceRepository = new AttendanceRepository(_context);
        }                                                       

//!This constructor can also be written this way

//todo private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs                            //All this 3 
                                .Include(g => g.Artist)     
                                .Include(g => g.Genre)                                        //can be in one line but
                                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);  //its always not a GOOD PRACTICE
                                                                        //TO SCROLL to the RIGHT.

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                               g.Artist.Name.Contains(query) ||
                               g.Genre.Name.Contains(query) ||
                               g.Venue.Contains(query));
            }


            //to load all ATTENDANCES teh USER is GOING
            string userId = User.Identity.GetUserId();
            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);



            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "ALL THE UPCOMING GIGS",
                SearchTerm = query,
                Attendances = attendances
             };

            return View("Gigs", viewModel);                                  //THEN FINALIZE BY PUTTING THE MODEL INSIDE THE VIEW (upcomingGigs)     
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}



