using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model.Positions;
using SSE_Reporting.Model.Positions.Levels;

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
        public DbSet<Position> Positions { get; set; }
        public DbSet<Level> Levels { get; set; }
    }
}
