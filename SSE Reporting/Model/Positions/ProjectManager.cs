using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model.Employments;
using SSE_Reporting.Model.Positions.Levels;

namespace SSE_Reporting.Model.Positions
{
	class ProjectManager : Position
	{

		private const string name = "PM";
		private const int id = 3;

		public ProjectManager(Level level) : base(id, name, level)
		{
		}

		public ProjectManager() : base(id, name, null)
		{
		}

        public override Employment GetEmployment()
        {
            return new OfficialEmployment();
        }
    }
}
