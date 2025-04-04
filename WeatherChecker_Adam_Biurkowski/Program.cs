using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherChecker_Adam_Biurkowski.Intrefaces;
using WeatherChecker_Adam_Biurkowski.Persistance;
using WeatherChecker_Adam_Biurkowski.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var secretKey = "your_secret_key_your_secret_key_your_secret_key_your_secret_key_your_secret_key_your_secret_key";

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSingleton<IJwtTokenService>(new JwtProviderService(secretKey));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AccountsDb"));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "YourApp",

            ValidateAudience = true,
            ValidAudience = "YourApp",

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();

