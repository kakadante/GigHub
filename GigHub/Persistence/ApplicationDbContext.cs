using System.Data.Entity;
using GigHub.Core.Models;
using GigHub.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        //todo Za NOTIFICATION
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

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
            modelBuilder.Configurations.Add(new GigConfiguration());


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)          //todo for FOLLOWERS
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(f => f.Followees)          //todo for FOLLOWEES
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>()
                .HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}