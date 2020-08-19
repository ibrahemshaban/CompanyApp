namespace Companyy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IT", "EmployID", "dbo.Employs");
            DropForeignKey("dbo.Salaries", "EmployID", "dbo.Employs");
            DropIndex("dbo.IT", new[] { "EmployID" });
            DropIndex("dbo.Salaries", new[] { "EmployID" });
            RenameColumn(table: "dbo.IT", name: "EmployID", newName: "Employ_Id");
            RenameColumn(table: "dbo.Salaries", name: "EmployID", newName: "Employ_Id");
            AddColumn("dbo.IT", "Attendance", c => c.Int(nullable: false));
            AddColumn("dbo.IT", "Departure", c => c.Int(nullable: false));
            AddColumn("dbo.IT", "UserName", c => c.Int(nullable: false));
            AddColumn("dbo.Salaries", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Salaries", "UserName", c => c.Int(nullable: false));
            AlterColumn("dbo.IT", "Employ_Id", c => c.Int());
            AlterColumn("dbo.Salaries", "Employ_Id", c => c.Int());
            CreateIndex("dbo.IT", "Employ_Id");
            CreateIndex("dbo.Salaries", "Employ_Id");
            AddForeignKey("dbo.IT", "Employ_Id", "dbo.Employs", "Id");
            AddForeignKey("dbo.Salaries", "Employ_Id", "dbo.Employs", "Id");
            DropColumn("dbo.IT", "Hours");
            DropColumn("dbo.IT", "Days");
            AlterStoredProcedure(
                "dbo.sp_InsertEmploy",
                p => new
                    {
                        name = p.String(),
                        adress = p.String(),
                        birthdate = p.DateTime(),
                        phone = p.Int(),
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
                        phone = p.Int(),
                        qualification = p.String(),
                        image = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Employs]
                      SET [UserName] = @name, [Adress] = @adress, [BirthDate] = @birthdate, [PhoneNumber] = @phone, [Qualification] = @qualification, [JobImage] = @image
                      WHERE ([Id] = @Id)"
            );
            
            AlterStoredProcedure(
                "dbo.sp_InsertIT",
                p => new
                    {
                        attendance = p.Int(),
                        departure = p.Int(),
                        UserName = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[IT]([Attendance], [Departure], [UserName], [Employ_Id])
                      VALUES (@attendance, @departure, @UserName, @Employ_Id)
                      
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
                        UserName = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[IT]
                      SET [Attendance] = @attendance, [Departure] = @departure, [UserName] = @UserName, [Employ_Id] = @Employ_Id
                      WHERE ([Id] = @Id)"
            );
            
            AlterStoredProcedure(
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
            
            AlterStoredProcedure(
                "dbo.sp_InsertSalary",
                p => new
                    {
                        salary = p.Decimal(precision: 18, scale: 2),
                        time = p.DateTime(),
                        Discount = p.Decimal(precision: 18, scale: 2),
                        UserName = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Salaries]([SalaryEmploy], [TimeSalaryEmpoly], [Discount], [UserName], [Employ_Id])
                      VALUES (@salary, @time, @Discount, @UserName, @Employ_Id)
                      
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
                        UserName = p.Int(),
                        Employ_Id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Salaries]
                      SET [SalaryEmploy] = @salary, [TimeSalaryEmpoly] = @time, [Discount] = @Discount, [UserName] = @UserName, [Employ_Id] = @Employ_Id
                      WHERE ([id] = @id)"
            );
            
            AlterStoredProcedure(
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
            AddColumn("dbo.IT", "Days", c => c.Int(nullable: false));
            AddColumn("dbo.IT", "Hours", c => c.Int(nullable: false));
            DropForeignKey("dbo.Salaries", "Employ_Id", "dbo.Employs");
            DropForeignKey("dbo.IT", "Employ_Id", "dbo.Employs");
            DropIndex("dbo.Salaries", new[] { "Employ_Id" });
            DropIndex("dbo.IT", new[] { "Employ_Id" });
            AlterColumn("dbo.Salaries", "Employ_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.IT", "Employ_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Salaries", "UserName");
            DropColumn("dbo.Salaries", "Discount");
            DropColumn("dbo.IT", "UserName");
            DropColumn("dbo.IT", "Departure");
            DropColumn("dbo.IT", "Attendance");
            RenameColumn(table: "dbo.Salaries", name: "Employ_Id", newName: "EmployID");
            RenameColumn(table: "dbo.IT", name: "Employ_Id", newName: "EmployID");
            CreateIndex("dbo.Salaries", "EmployID");
            CreateIndex("dbo.IT", "EmployID");
            AddForeignKey("dbo.Salaries", "EmployID", "dbo.Employs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IT", "EmployID", "dbo.Employs", "Id", cascadeDelete: true);
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
