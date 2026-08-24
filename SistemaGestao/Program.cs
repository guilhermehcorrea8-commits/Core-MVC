using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaGestao.Data;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// BANCO DE DADOS
// ===============================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ===============================
// MVC
// ===============================
builder.Services.AddControllersWithViews();

// ===============================
// IDENTITY
// ===============================
builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

// ===============================
// CONFIGURAÇÃO DO AMBIENTE
// ===============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// ===============================
// AUTENTICAÇÃO E AUTORIZAÇÃO
// ===============================
app.UseAuthentication();

app.UseAuthorization();

// ===============================
// ROTAS
// ===============================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();