namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class klkl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Salaries", "SalaryEmploy", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterStoredProcedure(
                "dbo.sp_InsertSalary",
                p => new
                    {
                        salary = p.Decimal(precision: 18, scale: 2),
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
            
            AlterStoredProcedure(
                "dbo.sp-UpdateSalary",
                p => new
                    {
                        id = p.Int(),
                        salary = p.Decimal(precision: 18, scale: 2),
                        time = p.DateTime(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Salaries]
                      SET [SalaryEmploy] = @salary, [TimeSalaryEmpoly] = @time, [Employ_Id] = @Employ_Id
                      WHERE ([id] = @id)"
            );
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Salaries", "SalaryEmploy", c => c.Single(nullable: false));
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
