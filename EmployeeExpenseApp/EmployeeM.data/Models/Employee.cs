using System;
using System.Collections.Generic;

namespace EmployeeM.data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ExpenseTbls = new HashSet<ExpenseTbl>();
        }

        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Guid? GenderId { get; set; }

        public virtual GenderTbl? Gender { get; set; }
        public virtual ICollection<ExpenseTbl> ExpenseTbls { get; set; }
    }
}
