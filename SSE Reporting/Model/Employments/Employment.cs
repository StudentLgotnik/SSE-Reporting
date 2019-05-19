using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model.Employments
{
	public abstract class Employment
	{

		private Dictionary<string, double> taxes;

		protected Employment(Dictionary<string, double> taxes)
		{
			this.taxes = taxes;
		}

		public Dictionary<string, double> getTaxes()
		{
			return taxes;
		}
	}
}
