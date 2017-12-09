namespace WebApi.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bar", "StreetAddress", c => c.String());
            AddColumn("dbo.Bar", "City", c => c.String());
            AddColumn("dbo.Bar", "Categories", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bar", "Categories");
            DropColumn("dbo.Bar", "City");
            DropColumn("dbo.Bar", "StreetAddress");
        }
    }
}
