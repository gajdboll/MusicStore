using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MusicStoreData.Data;
using MusicStore.Repository.IRepository;

namespace MusicStore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MusicStoreContext _context;
        internal DbSet<T> contextSet;
        public Repository(MusicStoreContext context)
        {
            _context = context;
            this.contextSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            contextSet.Add(entity);
        }

        public virtual T? Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = false)
        {
            IQueryable<T> query = tracked ? contextSet : contextSet.AsNoTracking();

            query = query.Where(filter);

            if (include != null)
            {
                query = include(query);
            }

            return query.FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = contextSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            contextSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            contextSet.RemoveRange(entity);
        }
        public void Update(T entity)
        {
            contextSet.Update(entity);

        }

    }
}
