using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeM.data.DTO
{
    public class AddOrEditEmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Guid? GenderId { get; set; }

        public ICollection<ExpenseAddEditDto>? Expenses { get; set; } 
    }
}
