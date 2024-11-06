using Microsoft.EntityFrameworkCore;
using Octavados.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddTiaIdentity()
                .AddCookie(options =>
                {
                    options.LoginPath = "/autenticacao/login";
                    options.AccessDeniedPath = "/autenticacao/logout";
                    options.LogoutPath = "/autenticacao/logout";
                });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseTiaIdentity();            
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
