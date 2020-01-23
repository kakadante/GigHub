using System;
using System.Data.Entity;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using GigHub.Repositories;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
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


            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gig's I'm attending",
                Attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }


        

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("index", "Home", new { query = viewModel.SearchTerm });
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


        //ii ndio inaHYPERLINK ARTIST-NAME, na kuleta DETAILS zake 
        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if(User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == userId);
            }
            return View("Details", viewModel);
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

            var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);
            //gig.Venue = viewModel.Venue;
            //gig.DateTime = viewModel.GetDateTime();
            //gig.GenreId = viewModel.Genre;

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

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
