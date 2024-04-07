using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T obj);
    T Update(T obj);
    T Delete(T obj);
}