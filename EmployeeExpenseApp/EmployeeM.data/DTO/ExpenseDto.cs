//using EmployeeM.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeM.data.DTO
{
    public class ExpenseDto
    {
        public Guid ExpenseId { get; set; }
        public string ExpenseName { get; set; } = null!;
        public Guid? TypeId { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }
        public Guid? EmployeeId { get; set; }
        public TypeDto? Type { get; set; }

    }
}
