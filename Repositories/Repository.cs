using System.Linq.Expressions;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
{
    public T Create(T obj)
    {
        context.Set<T>().Add(obj);
        // context.SaveChanges();
        return obj;

    }

    public T Delete(T obj)
    {
        context.Set<T>().Remove(obj);
        // context.SaveChanges();

        return obj;

    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().FirstOrDefault(predicate);
    }

    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().AsNoTracking().ToList();
    }

    public T Update(T obj)
    {
        context.Set<T>().Update(obj);
        // context.SaveChanges();

        return obj;

    }
}
