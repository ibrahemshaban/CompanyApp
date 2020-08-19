namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sacondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hours = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        Employ_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employs", t => t.Employ_Id)
                .Index(t => t.Employ_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IT", "Employ_Id", "dbo.Employs");
            DropIndex("dbo.IT", new[] { "Employ_Id" });
            DropTable("dbo.IT");
        }
    }
}
