using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Contacts
{
    public class Respoitory : IRepository
    {
        private readonly ContactsContext context;

        public Respoitory(ContactsContext context)
        {
            this.context = context;
        }

        public async Task<int> Delete<T>(int id, Expression<Func<T, int>> idProperty)
            where T : class, new()
        {
            var item = new T();
            var prop = (PropertyInfo)((MemberExpression)idProperty.Body).Member;
            prop.SetValue(item, id, null);
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public IEnumerable<T> Get<T>(Func<T, bool> predicate) where T : class, new()
        {
            return context.Set<T>().Where(predicate).ToList();
        }

        public async Task<T> Insert<T>(T item)
            where T : class, new()
        {
            context.Add(item);
            await context.SaveChangesAsync();
            return item;
        }


        public async Task<T> Update<T>(T item)
            where T : class, new()
        {
            context.Update(item);
            await context.SaveChangesAsync();
            return item;
        }
    }
}
