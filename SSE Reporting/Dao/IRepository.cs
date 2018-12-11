using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Dao
{
    public interface IRepository<T> : IDisposable where T : class
    {

        T save(T entity);
        T get(int id);
        T get(string line);
        ObservableCollection<T> getAll();
        T update(T entity);
        T delete(T entity);

    }
}
