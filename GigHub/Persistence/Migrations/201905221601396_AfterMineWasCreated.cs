namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AfterMineWasCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Followings", "Gig_Id", c => c.Int());
            CreateIndex("dbo.Followings", "Gig_Id");
            AddForeignKey("dbo.Followings", "Gig_Id", "dbo.Gigs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "Gig_Id", "dbo.Gigs");
            DropIndex("dbo.Followings", new[] { "Gig_Id" });
            DropColumn("dbo.Followings", "Gig_Id");
        }
    }
}
