//using EmployeeM.data.Models;
using EmployeeM.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeM.data.DTO
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Address { get; set; } = null!;
        public virtual ICollection<ExpenseDto>? ExpenseTbls { get; set; }
        public virtual GenderDto? Gender { get; set; }
    }
}
