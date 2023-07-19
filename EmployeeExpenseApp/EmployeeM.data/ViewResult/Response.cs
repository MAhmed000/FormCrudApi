using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeM.data.ViewResult
{
    public class Response<T>
    {
        public bool isSuccess { get; set; }
        public string? Message { get; set; }
        public T? Result;
    }
}
