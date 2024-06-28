using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Footballers.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
// добавление сервисов аутентификации
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Audience = "/Auth/Login";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };

        options.Events = new JwtBearerEvents
        { 
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["Auth"];
                return Task.CompletedTask;
            }
        };

    });



string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var db = service.GetService<ApplicationContext>();

    if (!(db.Teams.Any() && db.Footballers.Any() && db.Countries.Any()))
    {
        var country1 = new Country() { Name = "USA" };
        var country2 = new Country() { Name = "Russia" };
        var country3 = new Country() { Name = "Italy" };

        var team1 = new Team() { Name = "PSG" };
        var team2 = new Team() { Name = "Real Madrid" };
        var team3 = new Team() { Name = "Zenit" };

        var footballer1 = new Footballer() 
        { FirstName = "Christiano", SecondName = "Ronaldo", BirthDay = new DateOnly(1980, 5, 2), Gender = "Male", Team = team1, Country = country3 };
        var footballer2 = new Footballer()
        { FirstName = "Artem", SecondName = "Dzuba", BirthDay = new DateOnly(1975, 8, 12), Gender = "Male", Team = team3, Country = country2 };
        var footballer3 = new Footballer()
        { FirstName = "Zlatan", SecondName = "Ibrahimovich", BirthDay = new DateOnly(1978, 3, 22), Gender = "Male", Team = team2, Country = country1 };

        var person = new Person() { Email = "zaicev2400@mail.ru", Password = "vovalox123" };

        db.Add(person);
        db.AddRange(country1, country2, country3 );
        db.AddRange(team1, team2, team3);
        db.AddRange(footballer1, footballer2, footballer3);
        await db.SaveChangesAsync();
    }
}

app.Run();


public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}