using AutoMapper;
using EmployeeBLL.BLL.Interfaces;
using EmployeeM.data.DTO;
using EmployeeM.data.Models;
using EmployeeM.data.ViewResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee emp;
        public EmployeeController(IEmployee emp)
        {
            this.emp = emp;
        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> get()
        {
            try
            {
                var emplist = await emp.GetAllEmployees();
                if (emplist.isSuccess == false)
                {
                    return NotFound(emplist);
                }
                return Ok(emplist);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong..!");
            }

        }
        [HttpPost("AddNewEmployee")]
        public async Task<IActionResult> Post(AddOrEditEmployeeDto empl)
        {
            try
            {
                var result = await emp.AddNewEmployee(empl);
                if(result.isSuccess == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await emp.DeleteEmployee(id);
                if (result.isSuccess == false && result.Result == "Unauthorized")
                {
                    return Unauthorized(result);
                }
                if (result.isSuccess == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong..!");
            }
        }
        [HttpPut("UpdteEmployee")]
        public async Task<IActionResult> Update(AddOrEditEmployeeDto empl)
        {
            try
            {
                var result = await emp.UpdateEmployee(empl);
                if (result.isSuccess == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("GetAllGenders")]
        public async Task<IActionResult> GetGender()
        {
            var result =await emp.GetAllGenders();
            return Ok(result);
        }

        [HttpGet("GetAllTypes")]
        public async Task<IActionResult> GetTypes()
        {
            var result = await emp.GetAllTypes();
            return Ok(result);
        }
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetSingleEmployee(Guid id)
        {
            var emps=await emp.GetEmployeeById(id);
            return Ok(emps);
        }
        [HttpGet("GetExpense/{id}")]
        public async Task<IActionResult> GetExpense(Guid id)
        {
            var result=await emp.GetExpenseById(id);
            if (result.isSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
