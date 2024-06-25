using Footballers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// добавление сервисов аутентификации
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  // схема аутентификации - с помощью cookie
    .AddCookie(options => options.LoginPath = "/auth/login");      // подключение аутентификации с помощью cookie
builder.Services.AddAuthorization();

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