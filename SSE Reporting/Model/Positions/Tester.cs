using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model.Employments;
using SSE_Reporting.Model.Positions.Levels;

namespace SSE_Reporting.Model.Positions
{
	class Tester : Position
	{

		private const string name = "Tester";
		private const int id = 4;

		public Tester(Level level) : base(id, name, level)
		{
		}

		public Tester() : base(id, name, null)
		{
		}

        public override Employment GetEmployment()
        {
            return new SelfEmployment();
        }
    }
}
