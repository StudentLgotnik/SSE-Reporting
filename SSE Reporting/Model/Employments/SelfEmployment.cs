using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model.Employments
{
	public class SelfEmployment : Employment
	{
		public SelfEmployment() : base(new Dictionary<string, double> { { "ESV", 22 }, { "EP", 5 } })
		{
		}

		public SelfEmployment(Dictionary<string, double> taxes) : base(taxes)
		{
		}
	}
}
