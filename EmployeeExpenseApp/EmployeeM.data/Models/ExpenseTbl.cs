using System;
using System.Collections.Generic;

namespace EmployeeM.data.Models
{
    public partial class ExpenseTbl
    {
        public Guid ExpenseId { get; set; }
        public string ExpenseName { get; set; } = null!;
        public Guid? TypeId { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public Guid? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ExpenseType? Type { get; set; }
    }
}
