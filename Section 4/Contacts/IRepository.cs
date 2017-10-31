using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contacts
{
    public interface IRepository : IDisposable
    {
        Task<T> Insert<T>(T item)
            where T: class, new();

        Task<T> Update<T>(T item)
            where T : class, new();

        Task<int> Delete<T>(int id, Expression<Func<T, int>> idProperty)
            where T : class, new();

        IEnumerable<T> Get<T>(Func<T, bool> predicate)
            where T : class, new();
    }
}
