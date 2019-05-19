using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model;
using SSE_Reporting.Model.Employments;

namespace SSE_Reporting.Services
{
    class IncomeMaonthStrategy : PayStrategy
    {
        private const int minSalary = 4173;

        public double calcSalary(Employee employee)
        {
            double result = employee.Position.Level.Salary;
            foreach (var tax in employee.Position.GetEmployment().getTaxes())
            {
                if (tax.Key.Equals("ESV"))
                {
                    result = result - percent(minSalary, tax.Value);
                }

                result = result - percent(result, tax.Value);
            }

            return result;
        }

        public override string ToString()
        {
            return "Month - %";
        }

        private double percent(double value, double percent)
        {
            return (value * percent) / 100;
        }

     
    }
}
