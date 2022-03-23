using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        internal DbSet<T> db;

        public Repository(DbContext context)
        {
            this.context = context;
            this.db = context.Set<T>();
        }
        //-------------------------------------------------------------------------//

        public void Add(T entity)
        {
            db.Add(entity);
        }

        public T Get(int id)
        {
            return db.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = db;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            if (orderBy != null)
                return orderBy(query);

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = db;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            var EntityToRemove = db.Find(id);
            db.Remove(EntityToRemove);
        }

        public void Remove(T entity)
        {
            db.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            db.RemoveRange(entity);
        }
    }
}
