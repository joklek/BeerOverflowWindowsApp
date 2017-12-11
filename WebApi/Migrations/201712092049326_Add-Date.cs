namespace WebApi.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddDate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserRatings");
            AddColumn("dbo.UserRatings", "RatingDate", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.UserRatings", new[] { "BarId", "Username", "RatingDate" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserRatings");
            DropColumn("dbo.UserRatings", "RatingDate");
            AddPrimaryKey("dbo.UserRatings", new[] { "BarId", "Username" });
        }
    }
}
