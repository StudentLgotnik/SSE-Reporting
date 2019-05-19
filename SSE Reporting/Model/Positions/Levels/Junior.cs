using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model.Positions.Levels
{
	class Junior : Level
	{

		private const string level = "Junior";
		private const double salary = 9000;
		private const int id = 1;

		public Junior() : base(id, level, salary)
		{
		}
	}
}
