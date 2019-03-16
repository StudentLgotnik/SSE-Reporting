using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao.Impl
{
    class ReportImpl : IRepository<Report>
    {
        private static ReportImpl instance;
        private DBContext _dbContext;

        private ReportImpl(DBContext context)
        {
            _dbContext = context;
        }

        public static ReportImpl getInstance(DBContext context)
        {
            if (instance == null)
                instance = new ReportImpl(context);
            return instance;
        }

        public Report delete(Report entity)
        {
            _dbContext.Reports.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Report get(int id) => _dbContext.Reports.Find(id);


        public Report get(string line) => _dbContext.Reports.Where(user => user.Commect == line).FirstOrDefault();


        public ObservableCollection<Report> getAll()
        {
            return new ObservableCollection<Report>(_dbContext.Reports.ToList());
        }

        public Report save(Report entity)
        {
            _dbContext.Reports.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Report update(Report entity)
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
