using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE_Reporting.Model;
using SSE_Reporting.Model.Positions;

namespace SSE_Reporting.Dao.Impl
{
	class PositionImpl : IRepository<Position>
	{
		private static PositionImpl instance;
		private DBContext _dbContext;

		private PositionImpl(DBContext context)
		{
			_dbContext = context;
		}

		public static PositionImpl getInstance(DBContext context)
		{
			if (instance == null)
				instance = new PositionImpl(context);
			return instance;
		}

		public Position delete(Position entity)
		{
			_dbContext.Positions.Remove(entity);
			_dbContext.SaveChanges();
			return entity;
		}

		public Position get(int id) => _dbContext.Positions.Find(id);


		public Position get(string line) => _dbContext.Positions.Where(position => position.Name == line).FirstOrDefault();


		public ObservableCollection<Position> getAll()
		{
			return new ObservableCollection<Position>(_dbContext.Positions.ToList());
		}

		public Position save(Position entity)
		{
			_dbContext.Positions.Add(entity);
			_dbContext.SaveChanges();
			return entity;
		}

		public Position update(Position entity)
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
