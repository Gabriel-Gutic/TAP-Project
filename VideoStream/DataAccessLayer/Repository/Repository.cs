using System.Linq.Expressions;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(MyDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public T GetById(Guid id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
        public bool Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return false;
            }
            Remove(entity);
            return true;
        }
        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

		public bool Contains(Expression<Func<T, bool>> predicate)
		{
			return _entities.Any(predicate);
		}

		public int Count(Expression<Func<T, bool>> predicate)
		{
			return _entities.Count(predicate);
		}

		public int Count()
		{
            return _entities.Count(e => true);
		}
	}
}
