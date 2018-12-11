using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class AdminImpl : IRepository<Admin>
    {

        private DBContext _dbContext;

        public AdminImpl(DBContext context)
        {
            _dbContext = context;
        }

        public Admin delete(Admin entity)
        {
            _dbContext.Employees.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Admin get(int id) => (Admin)_dbContext.Employees.Find(id);


        public Admin get(string line) => (Admin)_dbContext.Employees.Where(user => user.Login == line).FirstOrDefault();


        public ObservableCollection<Admin> getAll()
        {
            HashSet<Admin> Admins = new HashSet<Admin>();
            foreach (Employee item in _dbContext.Employees)
            {
                Admins.Add(new Admin() { Id = item.Id, Login = item.Login, Password = item.Password, TimeOff =item.TimeOff, Sickness = item.Sickness, ProjectId = item.ProjectId, Role = item.Role });
            }
            return new ObservableCollection<Admin>(Admins);
        }

        public Admin save(Admin entity)
        {
            _dbContext.Employees.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Admin update(Admin entity)
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
