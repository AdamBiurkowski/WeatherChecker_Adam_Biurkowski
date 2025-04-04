namespace WeatherChecker_Adam_Biurkowski.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
