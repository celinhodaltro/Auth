using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Vinculo.Infrastructure.Persistence
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    }
}