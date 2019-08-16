using System;
using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Controllers.Api
{
    public class GigDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }
        public ICollection<Attendance> Attendances { get; private set; }
    }
}