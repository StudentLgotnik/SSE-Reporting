using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model;
using SSE_Reporting.Model.Positions.Levels;

namespace SSE_Reporting.Dao.Impl
{
	class LevelImpl : IRepository<Level>
	{

		private static LevelImpl instance;
		private DBContext _dbContext;

		private LevelImpl(DBContext context)
		{
			_dbContext = context;
		}

		public static LevelImpl getInstance(DBContext context)
		{
			if (instance == null)
				instance = new LevelImpl(context);
			return instance;
		}

		public Level delete(Level entity)
		{
			_dbContext.Levels.Remove(entity);
			_dbContext.SaveChanges();
			return entity;
		}

		public Level get(int id) => _dbContext.Levels.Find(id);


		public Level get(string line) => _dbContext.Levels.Where(Level => Level.Name == line).FirstOrDefault();


		public ObservableCollection<Level> getAll()
		{
			return new ObservableCollection<Level>(_dbContext.Levels.ToList());
		}

		public Level save(Level entity)
		{
			_dbContext.Levels.Add(entity);
			_dbContext.SaveChanges();
			return entity;
		}

		public Level update(Level entity)
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
