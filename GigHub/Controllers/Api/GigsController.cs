﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext(); }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.ArtistId == userId);

            //?--- KAMA GIG IKO CANCELED NA USER ANAITUMIA TENA nimeongeza ii CONDITION
                if (gig.IsCanceled)
                {
                    return NotFound();
                }
            //? --------------------------

            gig.Cancel();

            _context.SaveChanges();

            return Ok();
        }

    }
}
