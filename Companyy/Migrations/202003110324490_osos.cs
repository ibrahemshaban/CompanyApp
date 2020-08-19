namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class osos : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.sp_InsertSalary",
                p => new
                    {
                        salary = p.Single(),
                        time = p.DateTime(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Salaries]([SalaryEmploy], [TimeSalaryEmpoly], [Employ_Id])
                      VALUES (@salary, @time, @Employ_Id)
                      
                      DECLARE @id int
                      SELECT @id = [id]
                      FROM [dbo].[Salaries]
                      WHERE @@ROWCOUNT > 0 AND [id] = scope_identity()
                      
                      SELECT t0.[id]
                      FROM [dbo].[Salaries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[id] = @id"
            );
            
            CreateStoredProcedure(
                "dbo.sp-UpdateSalary",
                p => new
                    {
                        id = p.Int(),
                        salary = p.Single(),
                        time = p.DateTime(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Salaries]
                      SET [SalaryEmploy] = @salary, [TimeSalaryEmpoly] = @time, [Employ_Id] = @Employ_Id
                      WHERE ([id] = @id)"
            );
            
            CreateStoredProcedure(
                "dbo.sp-DeleteSalary",
                p => new
                    {
                        id = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Salaries]
                      WHERE (([id] = @id) AND (([Employ_Id] = @Employ_Id) OR ([Employ_Id] IS NULL AND @Employ_Id IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.sp-DeleteSalary");
            DropStoredProcedure("dbo.sp-UpdateSalary");
            DropStoredProcedure("dbo.sp_InsertSalary");
        }
    }
}
