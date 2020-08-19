using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Companyy.Models
{
    public class Employ
    {
        public int Id { get; set; }
        [DisplayName("اسم الموظف")]
        public string UserName { get; set; }
        [DisplayName("عنوان الموظف")]

        public string Adress { get; set; }
        [DisplayName("تاريخ ميلاد الموظف")]
        [DataType(DataType.Date)]
        public DateTime  BirthDate { get; set; }
        [DisplayName("رقم موبايل الموظف")]

        public string PhoneNumber { get; set; }
        [DisplayName("مؤهل الموظف")]

        public string Qualification { get; set; }
        [DisplayName("صورة الموظف")]

        public string JobImage { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<IT> ITs { get; set; }
        public virtual ICollection<HR> HRs { get; set; }



    }
}