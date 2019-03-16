using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class EmployeeImpl : IRepository<Employee>
    {
        private static EmployeeImpl instance;
        private DBContext _dbContext;

        private EmployeeImpl(DBContext context)
        {
            _dbContext = context;
        }

        public static EmployeeImpl getInstance(DBContext context)
        {
            if (instance == null)
                instance = new EmployeeImpl(context);
            return instance;
        }

        public Employee delete(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Employee get(int id) => _dbContext.Employees.Find(id);


        public Employee get(string line) => _dbContext.Employees.Where(user => user.Login == line).FirstOrDefault();


        public ObservableCollection<Employee> getAll()
        {
            return new ObservableCollection<Employee>(_dbContext.Employees);
        }

        public Employee save(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Employee update(Employee entity)
        {
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;                    
                }
            }
        }
    }
}
