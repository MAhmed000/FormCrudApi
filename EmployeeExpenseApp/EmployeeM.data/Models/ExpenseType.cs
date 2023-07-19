using System;
using System.Collections.Generic;

namespace EmployeeM.data.Models
{
    public partial class ExpenseType
    {
        public ExpenseType()
        {
            ExpenseTbls = new HashSet<ExpenseTbl>();
        }

        public Guid TypeId { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<ExpenseTbl> ExpenseTbls { get; set; }
    }
}
