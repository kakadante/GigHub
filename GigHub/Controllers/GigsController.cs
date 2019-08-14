using System;
using System.Data.Entity;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }



        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)   /*Here we should ega load genre*/
                .ToList();

            return View(gigs);
        }


        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followings = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(a => a.Followee)

                .ToList();

            return View(followings);
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)             /*Here we should ega load ARTIST*/
                .Include(g => g.Genre)              /*Here we should ega load GENRE*/
                .ToList();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gig's I'm attending"
            };

            return View("Gigs", viewModel);
        }



        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                 Genres = _context.Genres.ToList(),   //initializes genres dropdown list
                 Heading = "Add a Gig"
            };


            return View("GigForm", viewModel);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel    /*Initialize all entries in the form*/
            {
                 Heading = "Edit a Gig",
                 Id = gig.Id,
                 Genres = _context.Genres.ToList(),
                 Date = gig.DateTime.ToString("d MMM yyyy"),
                 Time = gig.DateTime.ToString("HH:mm"),
                 Genre = gig.GenreId,
                 Venue = gig.Venue
            };


            return View("GigForm", viewModel);   /*We can use the same view that we used to capture a gig*/
        }



        //todo CREATE action - kwa GIGFORMVIEWMODEL

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),    //The user here enters DATE and TIME uniquely from GigFormViewModel
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs"); //Before the user was directed to ("index", "Home") index in HomeController
                                                     //but we need to redirect them to mine after they Create a Gig
        }




        //todo UPDATE action - kwa GIGFORMVIEWMODEL

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g =>g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);
            //gig.Venue = viewModel.Venue;
            //gig.DateTime = viewModel.GetDateTime();
            //gig.GenreId = viewModel.Genre;

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs"); //Before the user was directed to ("index", "Home") index in HomeController
                                                     //but we need to redirect them to mine after they Create a Gig
        }
    }
}







//todo I Changed the CREATE.cshtml to GIGFORM.cshtml since the VIEW was being used in both the CREATE and EDIT interfaces

    //Actions mbili kabisa za mwisho tumeduplicate CREATE action ya MODELVIEW tukarename sacond one UPDATE

    //Apo mahali tunarender FIELDS tukatoa ili tutumie DETAILS tayari stored kwa DB
    //        var gig = _context.Gigs.Single(g => g.Id == viewModel.Id);
