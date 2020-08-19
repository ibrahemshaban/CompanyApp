namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gego : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IT", "Employ_Id", "dbo.Employs");
            DropForeignKey("dbo.Salaries", "Employ_Id", "dbo.Employs");
            DropIndex("dbo.IT", new[] { "Employ_Id" });
            DropIndex("dbo.Salaries", new[] { "Employ_Id" });
            RenameColumn(table: "dbo.IT", name: "Employ_Id", newName: "EmployID");
            RenameColumn(table: "dbo.Salaries", name: "Employ_Id", newName: "EmployID");
            AlterColumn("dbo.IT", "EmployID", c => c.Int(nullable: false));
            AlterColumn("dbo.Salaries", "EmployID", c => c.Int(nullable: false));
            CreateIndex("dbo.IT", "EmployID");
            CreateIndex("dbo.Salaries", "EmployID");
            AddForeignKey("dbo.IT", "EmployID", "dbo.Employs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Salaries", "EmployID", "dbo.Employs", "Id", cascadeDelete: true);
            AlterStoredProcedure(
                "dbo.sp_InsertIT",
                p => new
                    {
                        hours = p.Int(),
                        days = p.Int(),
                        EmployID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[IT]([Hours], [Days], [EmployID])
                      VALUES (@hours, @days, @EmployID)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[IT]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id] AS id
                      FROM [dbo].[IT] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            AlterStoredProcedure(
                "dbo.sp-UpdateIT",
                p => new
                    {
                        Id = p.Int(),
                        hours = p.Int(),
                        days = p.Int(),
                        EmployID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[IT]
                      SET [Hours] = @hours, [Days] = @days, [EmployID] = @EmployID
                      WHERE ([Id] = @Id)"
            );
            
            AlterStoredProcedure(
                "dbo.sp-DeleteIT",
                p => new
                    {
                        id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[IT]
                      WHERE ([Id] = @id)"
            );
            
            AlterStoredProcedure(
                "dbo.sp_InsertSalary",
                p => new
                    {
                        salary = p.Decimal(precision: 18, scale: 2),
                        time = p.DateTime(),
                        EmployID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Salaries]([SalaryEmploy], [TimeSalaryEmpoly], [EmployID])
                      VALUES (@salary, @time, @EmployID)
                      
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
                        EmployID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Salaries]
                      SET [SalaryEmploy] = @salary, [TimeSalaryEmpoly] = @time, [EmployID] = @EmployID
                      WHERE ([id] = @id)"
            );
            
            AlterStoredProcedure(
                "dbo.sp-DeleteSalary",
                p => new
                    {
                        id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Salaries]
                      WHERE ([id] = @id)"
            );
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Salaries", "EmployID", "dbo.Employs");
            DropForeignKey("dbo.IT", "EmployID", "dbo.Employs");
            DropIndex("dbo.Salaries", new[] { "EmployID" });
            DropIndex("dbo.IT", new[] { "EmployID" });
            AlterColumn("dbo.Salaries", "EmployID", c => c.Int());
            AlterColumn("dbo.IT", "EmployID", c => c.Int());
            RenameColumn(table: "dbo.Salaries", name: "EmployID", newName: "Employ_Id");
            RenameColumn(table: "dbo.IT", name: "EmployID", newName: "Employ_Id");
            CreateIndex("dbo.Salaries", "Employ_Id");
            CreateIndex("dbo.IT", "Employ_Id");
            AddForeignKey("dbo.Salaries", "Employ_Id", "dbo.Employs", "Id");
            AddForeignKey("dbo.IT", "Employ_Id", "dbo.Employs", "Id");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
