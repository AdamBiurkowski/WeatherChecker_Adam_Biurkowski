using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherChecker_Adam_Biurkowski.Intrefaces;
using WeatherChecker_Adam_Biurkowski.Models;
using WeatherChecker_Adam_Biurkowski.Persistance;

namespace WeatherChecker_Adam_Biurkowski.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly PasswordHasher<Account> _hasher = new();

        public AccountService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            if (await _dbContext.Accounts.AnyAsync(a => a.Email == email))
                return false;

            var account = new Account { Email = email };
            account.PasswordHash = _hasher.HashPassword(account, password);
            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Account?> GetByEmailAsync(string email) =>
        await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == email);

        public async Task<bool> VerifyPasswordAsync(string email, string password)
        {
            var account = await GetByEmailAsync(email);
            if (account == null) return false;
            var result = _hasher.VerifyHashedPassword(account, account.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
    
}
