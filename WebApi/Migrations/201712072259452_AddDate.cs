namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserRatings");
            AddColumn("dbo.UserRatings", "Date", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.UserRatings", new[] { "BarId", "Username", "Date" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserRatings");
            DropColumn("dbo.UserRatings", "Date");
            AddPrimaryKey("dbo.UserRatings", new[] { "BarId", "Username" });
        }
    }
}
