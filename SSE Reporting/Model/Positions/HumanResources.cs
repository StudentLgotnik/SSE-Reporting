using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model.Employments;
using SSE_Reporting.Model.Positions.Levels;

namespace SSE_Reporting.Model.Positions
{
	public class HumanResources : Position
	{

		private static string name = "HR";
		private const int id = 2;

		public HumanResources(Level level) : base(id, name, level)
		{
		}

		public HumanResources() : base(id, name, null)
		{
		}

        public override Employment GetEmployment()
        {
            return new OfficialEmployment();
        }
    }
}
