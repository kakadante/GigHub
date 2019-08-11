using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }   //todo ? -- nullable DATETIME used when GIG is UPDATED
        public string OriginalVenue { get; set; }

        [Required]                          //? -- making sure the column is not nullable
        public Gig Gig { get; set; }
    }
}