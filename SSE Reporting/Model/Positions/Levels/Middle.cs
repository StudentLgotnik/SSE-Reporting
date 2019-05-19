using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model.Positions.Levels
{
	class Middle : Level
	{

		private const string level = "Middle";
		private const double salary = 18000;
		private const int id = 2;

		public Middle() : base(id, level, salary)
		{
		}
	}
}
