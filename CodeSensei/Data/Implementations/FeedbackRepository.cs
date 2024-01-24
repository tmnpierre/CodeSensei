using CodeSensei.Data.Models;
using CodeSensei.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using CodeSensei.Data.Repositories.Interfaces;

namespace CodeSensei.Data.Implementations
{
    public class FeedbackRepository : IRepository<FeedbackRecord>
    {
        private readonly FeedbackContext _context;

        public FeedbackRepository(FeedbackContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FeedbackRecord entity)
        {
            await _context.FeedbackRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FeedbackRecords.FindAsync(id);
            if (entity != null)
            {
                _context.FeedbackRecords.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FeedbackRecord>> GetAllAsync()
        {
            return await _context.FeedbackRecords.ToListAsync();
        }

        public async Task<FeedbackRecord> GetByIdAsync(int id)
        {
            return await _context.FeedbackRecords.FindAsync(id);
        }

        public async Task UpdateAsync(FeedbackRecord entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
