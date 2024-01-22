using Microsoft.EntityFrameworkCore;
using CodeSensei.Data.Models;

namespace CodeSensei.Data.Contexts
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options)
            : base(options)
        {
        }

        public DbSet<FeedbackRecord> FeedbackRecords { get; set; }
    }
}
