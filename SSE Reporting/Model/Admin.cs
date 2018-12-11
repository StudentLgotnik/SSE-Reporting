using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class Admin : Employee
    {

        public Admin() : base()
        {
            Role = Role.Admin;
        }

        public Admin(string login, string password) : base(login, password)
        {
            Role = Role.Admin;
        }

    }
}
