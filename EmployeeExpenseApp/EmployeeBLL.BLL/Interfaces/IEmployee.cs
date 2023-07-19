using EmployeeM.data.DTO;
using EmployeeM.data.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBLL.BLL.Interfaces
{
    public interface IEmployee
    {
        Task<Response<List<EmployeeDto>>> GetAllEmployees();
        Task<Response<EmployeeDto>> GetEmployeeById(Guid id);
        Task<Response<string>> AddNewEmployee(AddOrEditEmployeeDto emp);
        Task<Response<string>> UpdateEmployee(AddOrEditEmployeeDto emp);
        Task<Response<string>> DeleteEmployee(Guid id);
        Task<Response<List<GenderDto>>> GetAllGenders();
        Task<Response<List<TypeDto>>> GetAllTypes();
        Task<Response<List<ExpenseDto>>> GetExpenseById(Guid id);

    }

}
