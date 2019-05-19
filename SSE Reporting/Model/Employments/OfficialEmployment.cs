using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model.Employments
{
	public class OfficialEmployment : Employment
	{
		public OfficialEmployment() : base(new Dictionary<string, double> { { "PodohodniyNalog", 18 }, { "VS", 1.5 } })
		{
		}

		public OfficialEmployment(Dictionary<string, double> taxes) : base(taxes)
		{
		}
	}
}
