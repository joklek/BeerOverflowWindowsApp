namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bar", "Categories", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bar", "Categories");
        }
    }
}
