using AutoMapper;
using EmployeeBLL.BLL.Interfaces;
using EmployeeM.data.DTO;
using EmployeeM.data.Models;
//using EmployeeM.data.Models;
using EmployeeM.data.ViewResult;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBLL.BLL.Repositories
{
    public class EmployeeService : IEmployee
    {
        private readonly Employee_DbContext _db;
        private readonly IMapper mapper;
        public EmployeeService(Employee_DbContext _db, IMapper mapper)
        {
            this._db = _db;
            this.mapper = mapper;
        }

        async Task<Response<string>> IEmployee.AddNewEmployee(AddOrEditEmployeeDto emp)
        {
            if (emp == null)
            {
                return new Response<string>()
                {
                    isSuccess = false,
                    Message="Provide the employee fields first..!"
                };
            }

            // Add New Employee
            var Newemp = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeName=emp.EmployeeName,
                Contact=emp.Contact,
                Address=emp.Address,
                GenderId=emp.GenderId
            };
            // Add New Expense

            if (emp.Expenses != null)
            {
                foreach (var expense in emp.Expenses)
                {
                    var newExpense = new ExpenseTbl()
                    {
                        ExpenseId=Guid.NewGuid(),
                        ExpenseName=expense.ExpenseName,
                        Date=expense.Date,
                        TypeId=expense.TypeId,
                        Cost=expense.Cost,
                        EmployeeId= Newemp.EmployeeId,
                    };
                    Newemp.ExpenseTbls.Add(newExpense);
                }
            }



            await _db.Employees.AddAsync(Newemp);
            var x=await _db.SaveChangesAsync();
            if (x > 0)
            {
                return new Response<string>()
                {
                    isSuccess=true,
                    Message="Success..!",
                    Result= "New Record Added..!"
                };
            }
            return new Response<string>()
            {
                isSuccess = false,
                Message="Failure..!",
                Result= "Record Not Added..!"
            };
        }

        async Task<Response<string>> IEmployee.DeleteEmployee(Guid id)
        {
            var findEmploee =await _db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (findEmploee != null)
            {
                var findExpense =await _db.ExpenseTbls.Where(x => x.EmployeeId == id).ToListAsync();
                if (findExpense != null)
                {
                    _db.ExpenseTbls.RemoveRange(findExpense);
                }
                _db.Employees.Remove(findEmploee);
                var result= await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<string>()
                    {
                        isSuccess=true,
                        Message="Success..!",
                        Result="One Record Deleted Successfully..!"
                    };
                }
                return new Response<string>()
                {
                    isSuccess = false,
                    Message = "Failure",
                    Result = "Record Not Deleted..!"
                };
            }
            return new Response<string>()
            {
                isSuccess=false,
                Message="Failure",
                Result="Unauthorized"
            };
        }

        async Task<Response<List<EmployeeDto>>> IEmployee.GetAllEmployees()
        {
            var emp_list = await _db.Employees
                .Include(x => x.ExpenseTbls)
                .ThenInclude(x=>x.Type)
                .Include(x => x.Gender)
                .ToListAsync();

            if (emp_list == null)
            {
                return new Response<List<EmployeeDto>>()
                {
                    isSuccess = false,
                    Message = "Failure",
                    Result = new List<EmployeeDto>()
                    {
                        new EmployeeDto() { }
                    }
                };
            }

            var emp_map_list = mapper.Map<List<EmployeeDto>>(emp_list);

            return new Response<List<EmployeeDto>>()
            {
                isSuccess = true,
                Message = "Success..!",
                Result = emp_map_list
            };
        }

        async Task<Response<List<GenderDto>>> IEmployee.GetAllGenders()
        {
            var result = await _db.GenderTbls.ToListAsync();

            var mapGender=mapper.Map<List<GenderDto>>(result);

            return new Response<List<GenderDto>>()
            {
                isSuccess = true,
                Message="Success",
                Result= mapGender
            };
        }

        async Task<Response<EmployeeDto>> IEmployee.GetEmployeeById(Guid id)
        
        {
            var result = await _db.Employees
               .Include(x=>x.ExpenseTbls)
               .Include(x=>x.Gender)
               .Where(x => x.EmployeeId == id)
               .FirstOrDefaultAsync();

            var mapData = mapper.Map<EmployeeDto>(result);

            return new Response<EmployeeDto>()
            {
                isSuccess = true,
                Message = "Success",
                Result=mapData
            };
        }

        async Task<Response<string>> IEmployee.UpdateEmployee(AddOrEditEmployeeDto emp)
        {
            if (emp == null)
            {
                return new Response<string>()
                {
                    isSuccess = false,
                    Message = "Failure",
                    Result = "Provide the fields first..!"
                };
            }

            var findEmployee = await _db.Employees
                .Include(x => x.ExpenseTbls)
                .FirstOrDefaultAsync(x => x.EmployeeId == emp.EmployeeId);

            if (findEmployee == null)
            {
                return new Response<string>()
                {
                    isSuccess = false,
                    Message = "Failure",
                    Result = "Unauthorized"
                };
            }

            // Update Employee
            findEmployee.EmployeeName = emp.EmployeeName;
            findEmployee.Address = emp.Address;
            findEmployee.Contact = emp.Contact;
            findEmployee.GenderId = emp.GenderId;

            // Now Update our expenses
            foreach (var expense in emp.Expenses)
            {
                var existingExpense = await _db.ExpenseTbls.Where(x=>x.ExpenseId==expense.ExpenseId).FirstOrDefaultAsync();

                if (existingExpense!=null)
                {
                    existingExpense.ExpenseName = expense.ExpenseName;
                    existingExpense.Cost = expense.Cost;
                    existingExpense.TypeId = expense.TypeId;
                    existingExpense.Date = expense.Date;
                    existingExpense.EmployeeId = emp.EmployeeId;
                }
                else
                {
                    var newExpense = new ExpenseTbl()
                    {
                        ExpenseId = Guid.NewGuid(),
                        ExpenseName = expense.ExpenseName,
                        Cost = expense.Cost,
                        TypeId = expense.TypeId,
                        Date = expense.Date,
                        EmployeeId = emp.EmployeeId,
                    };
                    await _db.ExpenseTbls.AddAsync(newExpense);
                }
            }

            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                return new Response<string>()
                {
                    isSuccess = true,
                    Message = "Success",
                    Result = "Updated Successfully...!"
                };
            }

            return new Response<string>()
            {
                isSuccess = false,
                Message = "Failure",
                Result = "Record Not Updated...!"
            };
        }

        async Task<Response<List<TypeDto>>> IEmployee.GetAllTypes()
        {
            var result =await _db.ExpenseTypes
                .ToListAsync();
            var mapData=mapper.Map<List<TypeDto>>(result);

            return new Response<List<TypeDto>>()
            {
                isSuccess = true,
                Message="Success",
                Result=mapData
            };
        }

        async Task<Response<List<ExpenseDto>>> IEmployee.GetExpenseById(Guid id)
        {
            var result = await _db.ExpenseTbls
                .Include(x=>x.Type)
                .Where(x=>x.EmployeeId== id)
                .ToListAsync();
            if (result == null)
            {
                return new Response<List<ExpenseDto>> { isSuccess = false, Message = "Failure" };
            }

            var mapData = mapper.Map<List<ExpenseDto>>(result);

            return new Response<List<ExpenseDto>>()
            {
                isSuccess = true,
                Message="Success",
                Result=mapData
            };
        }
    }

}
