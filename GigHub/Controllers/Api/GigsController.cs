﻿using System.Linq;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{

    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);



            //todo --- KAMA GIG IKO CANCELED NA USER ANAITUMIA TENA nimeongeza ii CONDITION
                if (gig.IsCanceled)
                {
                    return NotFound();
                }
            //todo --------------------------



            gig.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }

    }
}
