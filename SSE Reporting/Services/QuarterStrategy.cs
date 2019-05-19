using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model;

namespace SSE_Reporting.Services
{
    class QuarterStrategy : PayStrategy
    {
        public double calcSalary(Employee employee)
        {
            return employee.Position.Level.Salary * 3;
        }

        public override string ToString()
        {
            return "Quarter";
        }
    }
}
