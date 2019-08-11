using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; set; }   //todo ? -- nullable DATETIME used when GIG is UPDATED
        public string OriginalVenue { get; set; }

        [Required]                          //? -- making sure the column is not nullable
        public Gig Gig { get; private set; }


        protected Notification()
        {
        }

        public Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }
    }
}