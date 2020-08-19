using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Companyy.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employ>().MapToStoredProcedures(p=>p.Insert(sp=>sp.HasName("sp_InsertEmploy").Parameter(pm=>pm.UserName,"name").
            Parameter(pm=>pm.Adress,"adress").Parameter(pm=>pm.PhoneNumber,"phone").Parameter(pm=>pm.Qualification,"qualification").Parameter(pm => pm.BirthDate, "birthdate").Parameter(pm => pm.JobImage, "image").Result(rs=>rs.Id,"id"))
            .Update(sp=>sp.HasName("sp_UpdateEmploy").Parameter(pm=>pm.UserName,"name").Parameter(pm => pm.Adress, "adress").Parameter(pm => pm.PhoneNumber, "phone").Parameter(pm => pm.Qualification, "qualification").Parameter(pm => pm.BirthDate, "birthdate").Parameter(pm => pm.JobImage, "image"))
            .Delete(sp => sp.HasName("sp_DeleteEmploy").Parameter(rs => rs.Id, "id")));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Salary>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertSalary").Parameter(pm => pm.SalaryEmploy, "salary").Parameter(pm => pm.TimeSalaryEmpoly, "time").Parameter(pm => pm.Discount, "discount").Parameter(pm => pm.EmployID, "employId").Result(rs => rs.id, "id"))
            .Update(sp => sp.HasName("sp-UpdateSalary").Parameter(pm => pm.SalaryEmploy, "salary").Parameter(pm => pm.TimeSalaryEmpoly, "time").Parameter(pm => pm.Discount, "discount"))
            .Delete(sp => sp.HasName("sp-DeleteSalary").Parameter(rs => rs.id, "id")));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IT>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertIT").Parameter(pm => pm.Attendance, "attendance").Parameter(pm => pm.Departure, "departure").Parameter(pm => pm.EmployID, "employId").Result(rs => rs.Id, "id"))
            .Update(sp => sp.HasName("sp-UpdateIT").Parameter(pm => pm.Attendance, "attendance").Parameter(pm => pm.Departure, "departure"))
            .Delete(sp => sp.HasName("sp-DeleteIT").Parameter(rs => rs.Id, "id")));
        }



        public System.Data.Entity.DbSet<Companyy.Models.Employ> Employs { get; set; }

        public System.Data.Entity.DbSet<Companyy.Models.Salary> Salaries { get; set; }

        public System.Data.Entity.DbSet<Companyy.Models.IT> IT { get; set; }

        public System.Data.Entity.DbSet<Companyy.Models.HR> HRs { get; set; }
    }

}