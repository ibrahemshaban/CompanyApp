using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Companyy.Models
{
    public class HR
    {
        public int Id { get; set; }
        [DisplayName("اسم الموظف ")]

        public int EmployID { get; set; }

        [DisplayName("قسم الموظف")]

        public string Department { get; set; }
        [DisplayName("How many hours Work ?")]
        public int Hours { get; set; }
        [DisplayName("How many Days work ? ")]
        public int Days { get; set; }
        public virtual Employ Employ { get; set; }

    }
}