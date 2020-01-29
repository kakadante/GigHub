using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; } //todo ? -- nullable DATETIME used when GIG is UPDATED
        public string OriginalVenue { get; set; }
        public GigDto Gig { get; set; }
    }
}