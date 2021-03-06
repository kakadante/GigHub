﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set;}

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            //? --- Gig Ikicanceliwa tutengeneze NOTIFICATION kwa USER
            var notification = Notification.GigCanceled(this);

            //! After working on io Include apo juu, we wont need the below code, acha ni-comment tu.

            //var attendees = _context.Attendances
            //    .Where(a => a.GigId == gig.Id)
            //    .Select(a => a.Attendee)
            //    .ToList();

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
            //? ------------------------------------------------
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        { 
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}