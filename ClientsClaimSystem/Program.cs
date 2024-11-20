using ClientsClaimSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);

// Add HTTPS Redirection to enable secure connections
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7253; // Use the port on which your app is running
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout duration
    options.Cookie.HttpOnly = true; // Make the cookie HTTP-only
    options.Cookie.IsEssential = true; // Mark the session cookie as essential
});

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable session management
app.UseSession();

app.UseAuthorization();

// Map default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map claims-specific routes
app.MapControllerRoute(
    name: "claims",
    pattern: "Claims/{action=Index}/{id?}",
    defaults: new { controller = "Claims", action = "Index" });

// Start the application
app.Run();
