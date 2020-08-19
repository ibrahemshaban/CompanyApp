namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neww : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employs", "PhoneNumber", c => c.String());
            AlterStoredProcedure(
                "dbo.sp_InsertEmploy",
                p => new
                    {
                        name = p.String(),
                        adress = p.String(),
                        birthdate = p.DateTime(),
                        phone = p.String(),
                        qualification = p.String(),
                        image = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Employs]([UserName], [Adress], [BirthDate], [PhoneNumber], [Qualification], [JobImage])
                      VALUES (@name, @adress, @birthdate, @phone, @qualification, @image)
                      
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
                        birthdate = p.DateTime(),
                        phone = p.String(),
                        qualification = p.String(),
                        image = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Employs]
                      SET [UserName] = @name, [Adress] = @adress, [BirthDate] = @birthdate, [PhoneNumber] = @phone, [Qualification] = @qualification, [JobImage] = @image
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employs", "PhoneNumber", c => c.Int(nullable: false));
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
