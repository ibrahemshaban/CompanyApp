namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goo : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.sp_InsertIT",
                p => new
                    {
                        hours = p.Int(),
                        days = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[IT]([Hours], [Days], [Employ_Id])
                      VALUES (@hours, @days, @Employ_Id)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[IT]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id] AS id
                      FROM [dbo].[IT] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.sp-UpdateIT",
                p => new
                    {
                        Id = p.Int(),
                        hours = p.Int(),
                        days = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[IT]
                      SET [Hours] = @hours, [Days] = @days, [Employ_Id] = @Employ_Id
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.sp-DeleteIT",
                p => new
                    {
                        id = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[IT]
                      WHERE (([Id] = @id) AND (([Employ_Id] = @Employ_Id) OR ([Employ_Id] IS NULL AND @Employ_Id IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.sp-DeleteIT");
            DropStoredProcedure("dbo.sp-UpdateIT");
            DropStoredProcedure("dbo.sp_InsertIT");
        }
    }
}
