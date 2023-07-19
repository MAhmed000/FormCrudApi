using AutoMapper;
using EmployeeM.data.DTO;
using EmployeeM.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeM.data.MapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<ExpenseDto, ExpenseTbl>().ReverseMap();
            CreateMap<GenderDto, GenderTbl>().ReverseMap();
            CreateMap<TypeDto, ExpenseType>().ReverseMap();
            CreateMap<ExpenseAddEditDto,ExpenseTbl>().ReverseMap();
        }
    }
}
