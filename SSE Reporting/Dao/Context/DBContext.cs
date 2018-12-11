using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class DBContext : DbContext
    {
        public DBContext()
            : base("DBConnection")
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
