using System.Linq.Expressions;

namespace Bookify.Repositary
{
    public interface IRepositary<T > where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T,bool>> func);
        void Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
    }
}
