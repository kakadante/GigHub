using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {

        public GigConfiguration()
        {
            //todo ------------This SECTION--------------

            Property(g => g.ArtistId)
                .IsRequired();

            Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);

            Property(g => g.GenreId)
                .IsRequired();


            //todo -------------------------To this SECTION Here -----------------------------  
        }

    }
}