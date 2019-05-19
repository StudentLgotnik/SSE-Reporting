using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model.Employments;
using SSE_Reporting.Model.Positions.Levels;

namespace SSE_Reporting.Model.Positions
{
	class Developer : Position
	{

		private const string name = "Dev";
		private const int id = 1;

		public Developer(Level level) : base(id, name, level)
		{
		}

		public Developer() : base(id, name, null)
		{
		}

        public override Employment GetEmployment()
        {
            return new SelfEmployment();
        }
    }
}
