using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Services
{
	interface PayStrategy
	{
        //interface for strategy pattern
		double calcSalary(Employee employee);
	}
}
