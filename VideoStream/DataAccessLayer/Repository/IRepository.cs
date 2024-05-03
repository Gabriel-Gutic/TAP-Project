using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace DataAccessLayer.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        bool Contains(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        bool Delete(Guid id);
        void SaveChanges();
    }
}
