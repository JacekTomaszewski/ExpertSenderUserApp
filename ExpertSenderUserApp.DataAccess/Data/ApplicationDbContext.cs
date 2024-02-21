using ExpertSenderUserApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpertSenderUserApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
