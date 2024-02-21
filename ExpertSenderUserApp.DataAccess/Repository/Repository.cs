using ExpertSenderUserApp.DataAccess;
using ExpertSenderUserApp.Interfaces;
using ExpertSenderUserApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpertSenderUserApp.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = _context.Set<T>().OrderBy(x => x.DateCreated);
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> filter)
        {
            IQueryable<T>  query = _context.Set<T>().Where(filter);
            return await query.AnyAsync();
        }
    }
}
