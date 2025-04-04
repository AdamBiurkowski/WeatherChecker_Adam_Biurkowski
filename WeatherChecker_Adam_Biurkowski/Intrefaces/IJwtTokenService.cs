namespace WeatherChecker_Adam_Biurkowski.Intrefaces
{
    public interface IJwtTokenService
    {
        public string GenerateToken(string email);
    }
}
