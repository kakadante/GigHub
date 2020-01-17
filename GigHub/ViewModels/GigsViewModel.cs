using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
        public class GigsViewModel
        {
            public bool ShowActions { get; internal set; }
            public IEnumerable<Gig> UpcomingGigs { get; internal set; }
            public string Heading { get; set; }
            public string SearchTerm { get; set; }
            public ILookup<int, Attendance> Attendances { get; internal set; }
    }
}