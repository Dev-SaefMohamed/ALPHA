using Hero.Data;
using Hero.Data.Repositories;
using Hero.Data.DataSeeder;
using Hero.Models.Entities;
using Hero.Services.Implementation;
using Hero.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Database configuration
// =======================
var connectionString = builder.Configuration.GetConnectionString("HeroConnection")
                       ?? throw new InvalidOperationException("Connection string 'HeroConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// =======================
// Identity configuration
// =======================
// Use ApplicationUser instead of IdentityUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // No email confirmation
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

// =======================
// Add MVC
// =======================
builder.Services.AddControllersWithViews();

// =======================
// DI for repositories and services
// =======================
builder.Services.AddScoped<ICorporateEmissionRepository, CorporateEmissionRepository>();
builder.Services.AddScoped<ICorporateEmissionService, CorporateEmissionService>();
builder.Services.AddScoped<DataSeeder>(); // Add seeder

var app = builder.Build();

// =======================
// Run DataSeeder at startup
// =======================
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}

// =======================
// Configure middleware
// =======================
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // <-- Important for Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Role seeder
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}


app.Run();
