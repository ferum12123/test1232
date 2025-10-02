using Asystem.Infrastructure.Data;
using Asystem.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext - SQLite file asystem.db in application folder
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=asystem.db"));

var app = builder.Build();

// Ensure DB & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Create DB/tables based on model if migrations are not set up yet
    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// В разработке отключаем HTTPS-редирект, чтобы не требовать настроенный https-порт
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthorization();
app.MapControllers();

// Optionally set URL (if you want HTTP on 5000)
app.Urls.Add("http://localhost:5000");

// Корневой маршрут: редирект на Swagger для удобства
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

public partial class Program { } // for integration tests
