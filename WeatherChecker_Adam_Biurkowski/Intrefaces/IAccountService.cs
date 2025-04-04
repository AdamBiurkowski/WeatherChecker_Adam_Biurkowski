using WeatherChecker_Adam_Biurkowski.Models;

namespace WeatherChecker_Adam_Biurkowski.Intrefaces
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(string email, string password);
        Task<Account?> GetByEmailAsync(string email);
        Task<bool> VerifyPasswordAsync(string email, string password);
    }
}
