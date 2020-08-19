namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kokoko : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserType");
            RenameStoredProcedure(name: "dbo.Employ_Insert", newName: "sp_InsertEmploy");
            RenameStoredProcedure(name: "dbo.Employ_Update", newName: "sp_UpdateEmploy");
            RenameStoredProcedure(name: "dbo.Employ_Delete", newName: "sp_DeleteEmploy");
            AlterStoredProcedure(
                "dbo.sp_InsertEmploy",
                p => new
                    {
                        name = p.String(),
                        adress = p.String(),
                        phone = p.Int(),
                        qualification = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Employs]([UserName], [Adress], [PhoneNumber], [Qualification])
                      VALUES (@name, @adress, @phone, @qualification)
                      
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
                        phone = p.Int(),
                        qualification = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Employs]
                      SET [UserName] = @name, [Adress] = @adress, [PhoneNumber] = @phone, [Qualification] = @qualification
                      WHERE ([Id] = @Id)"
            );
            
            AlterStoredProcedure(
                "dbo.sp_DeleteEmploy",
                p => new
                    {
                        id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Employs]
                      WHERE ([Id] = @id)"
            );
            
        }
        
        public override void Down()
        {
            RenameStoredProcedure(name: "dbo.sp_DeleteEmploy", newName: "Employ_Delete");
            RenameStoredProcedure(name: "dbo.sp_UpdateEmploy", newName: "Employ_Update");
            RenameStoredProcedure(name: "dbo.sp_InsertEmploy", newName: "Employ_Insert");
            AddColumn("dbo.AspNetUsers", "UserType", c => c.String());
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
