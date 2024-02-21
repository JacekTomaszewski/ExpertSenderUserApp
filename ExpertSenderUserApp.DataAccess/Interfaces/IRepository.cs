using ExpertSenderUserApp.Models.Entities;
using System.Linq.Expressions;

namespace ExpertSenderUserApp.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        void Update(T entity);
        void Remove(T entity);
        void Add(T entity);
        void Save();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> filter);
    }
}
