using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class ProjectImpl : IRepository<Project>
    {
        private DBContext _dbContext;

        public ProjectImpl(DBContext context)
        {
            _dbContext = context;
        }

        public Project delete(Project entity)
        {
            _dbContext.Projects.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Project get(int id) => _dbContext.Projects.Find(id);


        public Project get(string line) => _dbContext.Projects.Where(user => user.Name == line).FirstOrDefault();


        public ObservableCollection<Project> getAll()
        {
            return new ObservableCollection<Project>(_dbContext.Projects.ToList());
        }

        public Project save(Project entity)
        {
            _dbContext.Projects.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Project update(Project entity)
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
