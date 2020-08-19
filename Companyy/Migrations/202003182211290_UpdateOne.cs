namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Department = c.String(),
                        Hours = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        Employ_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employs", t => t.Employ_Id)
                .Index(t => t.Employ_Id);
            
            AddColumn("dbo.Employs", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employs", "JobImage", c => c.String());
            AlterStoredProcedure(
                "dbo.sp_InsertEmploy",
                p => new
                    {
                        name = p.String(),
                        adress = p.String(),
                        BirthDate = p.DateTime(),
                        phone = p.Int(),
                        qualification = p.String(),
                        JobImage = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Employs]([UserName], [Adress], [BirthDate], [PhoneNumber], [Qualification], [JobImage])
                      VALUES (@name, @adress, @BirthDate, @phone, @qualification, @JobImage)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Employs]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id] AS id
                      FROM [dbo].[Employs] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            AlterStoredProcedure(
                "dbo.sp_UpdateEmploy",
                p => new
                    {
                        Id = p.Int(),
                        name = p.String(),
                        adress = p.String(),
                        BirthDate = p.DateTime(),
                        phone = p.Int(),
                        qualification = p.String(),
                        JobImage = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Employs]
                      SET [UserName] = @name, [Adress] = @adress, [BirthDate] = @BirthDate, [PhoneNumber] = @phone, [Qualification] = @qualification, [JobImage] = @JobImage
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HRs", "Employ_Id", "dbo.Employs");
            DropIndex("dbo.HRs", new[] { "Employ_Id" });
            DropColumn("dbo.Employs", "JobImage");
            DropColumn("dbo.Employs", "BirthDate");
            DropTable("dbo.HRs");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
