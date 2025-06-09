// Auth.Domain.Interfaces
using Auth.Domain.Entities;

public interface IAuthRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task<bool> ExistsAsync(string email);
}
