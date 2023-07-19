using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeM.data.DTO
{
    public class GenderDto
    {
        public Guid GenderId { get; set; }
        public string Gender { get; set; } = null!;
    }
}
