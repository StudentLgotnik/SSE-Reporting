using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model;
using SSE_Reporting.Model.Employments;

namespace SSE_Reporting.Services
{
    class YearStrategy : PayStrategy
    {
        public double calcSalary(Employee employee)
        {
            return employee.Position.Level.Salary * 12;
        }

        public override string ToString()
        {
            return "Year";
        }
    }
}
