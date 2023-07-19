using System;
using System.Collections.Generic;

namespace EmployeeM.data.Models
{
    public partial class GenderTbl
    {
        public GenderTbl()
        {
            Employees = new HashSet<Employee>();
        }

        public Guid GenderId { get; set; }
        public string Gender { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
