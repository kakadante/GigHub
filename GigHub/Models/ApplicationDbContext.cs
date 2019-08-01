using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        //TUME-ADD II (FLUENT API) KUoverride io domain model (ATTENDANCE) ndio ikue na rltnship ya MANY TO MANY
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)            //todo for GIG
                .WithMany()
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)          //todo for FOLLOWERS
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(f => f.Followees)          //todo for FOLLOWEES
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false); 

            base.OnModelCreating(modelBuilder);
        }
    }
}