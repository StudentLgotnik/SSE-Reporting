using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SSE_Reporting.Dao.Impl
{
    class TaskImpl : IRepository<Task>
    {
        private static TaskImpl instance;
        private DBContext _dbContext;

        private TaskImpl(DBContext context)
        {
            _dbContext = context;
        }

        public static TaskImpl getInstance(DBContext context)
        {
            if (instance == null)
                instance = new TaskImpl(context);
            return instance;
        }

        public Task delete(Task entity)
        {
            _dbContext.Tasks.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Task get(int id) => _dbContext.Tasks.Find(id);


        public Task get(string line) => _dbContext.Tasks.Where(user => user.Name == line).FirstOrDefault();


        public ObservableCollection<Task> getAll()
        {
            return new ObservableCollection<Task>(_dbContext.Tasks.ToList());
        }

        public Task save(Task entity)
        {
            _dbContext.Tasks.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Task update(Task entity)
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
