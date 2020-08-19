namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fiveeee : DbMigration
    {
        public override void Up()
        {
            AlterStoredProcedure(
                "dbo.sp_InsertIT",
                p => new
                    {
                        attendance = p.Int(),
                        departure = p.Int(),
                        employId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[IT]([Attendance], [Departure], [EmployID])
                      VALUES (@attendance, @departure, @employId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[IT]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id] AS id
                      FROM [dbo].[IT] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            AlterStoredProcedure(
                "dbo.sp_InsertSalary",
                p => new
                    {
                        salary = p.Decimal(precision: 18, scale: 2),
                        time = p.DateTime(),
                        discount = p.Decimal(precision: 18, scale: 2),
                        employId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Salaries]([SalaryEmploy], [TimeSalaryEmpoly], [Discount], [EmployID])
                      VALUES (@salary, @time, @discount, @employId)
                      
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
                        discount = p.Decimal(precision: 18, scale: 2),
                        EmployID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Salaries]
                      SET [SalaryEmploy] = @salary, [TimeSalaryEmpoly] = @time, [Discount] = @discount, [EmployID] = @EmployID
                      WHERE ([id] = @id)"
            );
            
        }
        
        public override void Down()
        {
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
