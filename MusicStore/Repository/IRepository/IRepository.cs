using MusicStore.Repository.IRepository;
using MusicStoreData.Models.CMS;
using System.Linq.Expressions;

namespace MusicStore.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,Func<IQueryable<T>, IQueryable<T>>? include = null);
        T? Get(Expression<Func<T, bool>> filter,Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
