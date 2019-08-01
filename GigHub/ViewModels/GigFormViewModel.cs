using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using GigHub.Controllers;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {

        public int Id { get; set; } /*we created this property for the ACTION property below to know if we are CREATING or UPDATING a gig*/
                                    //Io public string  Action chini kabisa kwa ii Model--zoom pia @using (Html.BeginForm(Model.Action, "Gigs")) kwa GigForm


        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {

                //todo Izi ni za kutoa MAGIC STRINGS ueke expression in that ukichange code haita-break.

                Expression<Func<GigsController, ActionResult>> update = (c => c.Update(this)); //For Update Action
                                                               //unaeza eka NULL apa --- Update(NULL))

                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(this)); //For Update Action

                //todo ADI APA

                //? so afterwards
                var action =  (Id != 0) ? update : create;

                //todo we can execute this at RUN~TIME

                return (action.Body as MethodCallExpression).Method.Name;

                //alternatively this can be used
                //var actionName = (action.Body as MethodCallExpression).Method.Name;



                return (Id != 0) ? "Update" : "Create";
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}
