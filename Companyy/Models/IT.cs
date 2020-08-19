using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Companyy.Models
{
    public class IT
    {
        public int Id { get; set; }
        [DisplayName("وقت حضور الموظف ")]
        public int Attendance { get; set; }
        [DisplayName("وقت انصراف الموظف")]
        public int Departure { get; set; }
        public int EmployID { get; set; }

        public virtual Employ Employ { get; set; }


    }
}