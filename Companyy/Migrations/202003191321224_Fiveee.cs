namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fiveee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HRs", "Employ_Id", "dbo.Employs");
            DropForeignKey("dbo.IT", "Employ_Id", "dbo.Employs");
            DropForeignKey("dbo.Salaries", "Employ_Id", "dbo.Employs");
            DropIndex("dbo.HRs", new[] { "Employ_Id" });
            DropIndex("dbo.IT", new[] { "Employ_Id" });
            DropIndex("dbo.Salaries", new[] { "Employ_Id" });
            RenameColumn(table: "dbo.HRs", name: "Employ_Id", newName: "EmployID");
            RenameColumn(table: "dbo.IT", name: "Employ_Id", newName: "EmployID");
            RenameColumn(table: "dbo.Salaries", name: "Employ_Id", newName: "EmployID");
            AlterColumn("dbo.HRs", "EmployID", c => c.Int(nullable: false));
            AlterColumn("dbo.IT", "EmployID", c => c.Int(nullable: false));
            AlterColumn("dbo.Salaries", "EmployID", c => c.Int(nullable: false));
            CreateIndex("dbo.HRs", "EmployID");
            CreateIndex("dbo.IT", "EmployID");
            CreateIndex("dbo.Salaries", "EmployID");
            AddForeignKey("dbo.HRs", "EmployID", "dbo.Employs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IT", "EmployID", "dbo.Employs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Salaries", "EmployID", "dbo.Employs", "Id", cascadeDelete: true);
            DropColumn("dbo.HRs", "UserName");
            DropColumn("dbo.IT", "UserName");
            DropColumn("dbo.Salaries", "UserName");
            AlterStoredProcedure(
                "dbo.sp_InsertIT",
                p => new
                    {
                        attendance = p.Int(),
                        departure = p.Int(),
                        EmployID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[IT]([Attendance], [Departure], [EmployID])
                      VALUES (@attendance, @departure, @EmployID)
                      
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
                        attendance = p.Int(),
                        departure = p.Int(),
                        EmployID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[IT]
                      SET [Attendance] = @attendance, [Departure] = @departure, [EmployID] = @EmployID
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
                        Discount = p.Decimal(precision: 18, scale: 2),
                        EmployID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Salaries]([SalaryEmploy], [TimeSalaryEmpoly], [Discount], [EmployID])
                      VALUES (@salary, @time, @Discount, @EmployID)
                      
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
                        Discount = p.Decimal(precision: 18, scale: 2),
                        EmployID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Salaries]
                      SET [SalaryEmploy] = @salary, [TimeSalaryEmpoly] = @time, [Discount] = @Discount, [EmployID] = @EmployID
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
            AddColumn("dbo.Salaries", "UserName", c => c.Int(nullable: false));
            AddColumn("dbo.IT", "UserName", c => c.Int(nullable: false));
            AddColumn("dbo.HRs", "UserName", c => c.String());
            DropForeignKey("dbo.Salaries", "EmployID", "dbo.Employs");
            DropForeignKey("dbo.IT", "EmployID", "dbo.Employs");
            DropForeignKey("dbo.HRs", "EmployID", "dbo.Employs");
            DropIndex("dbo.Salaries", new[] { "EmployID" });
            DropIndex("dbo.IT", new[] { "EmployID" });
            DropIndex("dbo.HRs", new[] { "EmployID" });
            AlterColumn("dbo.Salaries", "EmployID", c => c.Int());
            AlterColumn("dbo.IT", "EmployID", c => c.Int());
            AlterColumn("dbo.HRs", "EmployID", c => c.Int());
            RenameColumn(table: "dbo.Salaries", name: "EmployID", newName: "Employ_Id");
            RenameColumn(table: "dbo.IT", name: "EmployID", newName: "Employ_Id");
            RenameColumn(table: "dbo.HRs", name: "EmployID", newName: "Employ_Id");
            CreateIndex("dbo.Salaries", "Employ_Id");
            CreateIndex("dbo.IT", "Employ_Id");
            CreateIndex("dbo.HRs", "Employ_Id");
            AddForeignKey("dbo.Salaries", "Employ_Id", "dbo.Employs", "Id");
            AddForeignKey("dbo.IT", "Employ_Id", "dbo.Employs", "Id");
            AddForeignKey("dbo.HRs", "Employ_Id", "dbo.Employs", "Id");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
