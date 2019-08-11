using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column (Order = 1)]                        //todo - For COMPOSITE PRIMARY keys (Primary Key Mbili) - inabidi umeeka [Key] na [Column Order]
        public string UserId { get; private set; }


        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; set; }

        public Notification Notification { get; set; }

        public bool IsRead { get; set; }



        protected UserNotification()  //todo Ukitengeneza CONSTRUCTOR customed yako kama io apo chini unaunda ii ya EF ndio ichukue yako
        {                               //! Badala ya PUBLIC unaichange inakua PROTECTED ndio isiwai tumiwa, ni DEFAULT tu
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (notification == null)
                throw new ArgumentNullException("notification");

            User = user;
            Notification = notification;
        }
    }
}