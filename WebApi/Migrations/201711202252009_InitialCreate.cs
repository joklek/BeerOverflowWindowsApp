namespace WebApi.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bar",
                c => new
                    {
                        BarId = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 50),
                        AvgRating = c.Single(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BarId);
            
            CreateTable(
                "dbo.Synonym",
                c => new
                    {
                        BarId = c.String(nullable: false, maxLength: 50),
                        Synonym = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.BarId, t.Synonym })
                .ForeignKey("dbo.Bar", t => t.BarId, cascadeDelete: true)
                .Index(t => t.BarId);
            
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        BarId = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 30),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => new { t.BarId, t.Username })
                .ForeignKey("dbo.Bar", t => t.BarId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.Username, cascadeDelete: true)
                .Index(t => t.BarId)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRatings", "Username", "dbo.User");
            DropForeignKey("dbo.UserRatings", "BarId", "dbo.Bar");
            DropForeignKey("dbo.Synonym", "BarId", "dbo.Bar");
            DropIndex("dbo.UserRatings", new[] { "Username" });
            DropIndex("dbo.UserRatings", new[] { "BarId" });
            DropIndex("dbo.Synonym", new[] { "BarId" });
            DropTable("dbo.User");
            DropTable("dbo.UserRatings");
            DropTable("dbo.Synonym");
            DropTable("dbo.Bar");
        }
    }
}
