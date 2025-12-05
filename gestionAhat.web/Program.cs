using gestionsyndic.web.Models;
using gestionsyndic.web.newfolder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur
builder.Services.AddControllersWithViews();

// Ajouter le DbContext pour la base de données SQL Server
builder.Services.AddDbContext<GestionsyndicContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Active la sécurité HSTS en production
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Sert les fichiers CSS, JS, images, etc.

app.UseRouting();

app.UseAuthorization();

// Configuration des routes par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
