using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;                  //From Here to...

        public HomeController()                    /*--This is Just a constructor (CTOR +TAB*2)--*/
        {
                _context = new ApplicationDbContext();
        }                                                       //HERE ->   All this is retrieving data from database

//This constructor can also be written this way

// private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs                            //All this 3 
                                .Include(g => g.Artist)     
                                .Include(g => g.Genre)                                        //can be in one line but
                                .Where(g => g.DateTime > DateTime.Now);  //its always not a GOOD PRACTICE
                                                                        //TO SCROLL to the RIGHT.

             var viewModel = new GigsViewModel
             {
                 UpcomingGigs = upcomingGigs,
                 ShowActions = User.Identity.IsAuthenticated,
                 Heading = "ALL THE UPCOMING GIGS"
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



