namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSynonyms : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Synonym", "BarId", "dbo.Bar");
            DropIndex("dbo.Synonym", new[] { "BarId" });
            DropTable("dbo.Synonym");
        }
    }
}
