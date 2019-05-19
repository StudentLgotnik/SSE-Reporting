using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model.Positions.Levels
{
	class Senior : Level
	{

		private const string level = "Senior";
		private const double salary = 25000;
		private const int id = 3;

		public Senior() : base(id, level, salary)
		{
		}

	}
}
