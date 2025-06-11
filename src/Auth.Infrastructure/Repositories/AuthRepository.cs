
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Vinculo.Infrastructure.Persistence;

namespace Auth.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == email);
        }

        public async Task<bool> ValidateUserAsync(string email, string passwordHash)
        {
            return await _context.Users.AnyAsync(u => u.Username == email && u.PasswordHash == passwordHash);
        }
    }
}