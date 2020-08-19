using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Companyy.Models
{
    public class Salary
    {
        public int id { get; set; }
        [DisplayName("مرتب الموظف")]

        public decimal SalaryEmploy { get; set; }
        [DisplayName("تاريخ راتب الموظف الموظف")]
        [DataType(DataType.Date)]

        public DateTime TimeSalaryEmpoly { get; set; }
        [DisplayName("خصم علي الموظف")]

        public decimal Discount { get; set; }
        [DisplayName("Name Employ ")]
        public int EmployID { get; set; }
        public virtual Employ Employ { get; set; }


    }
}