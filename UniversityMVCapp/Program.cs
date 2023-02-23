using Microsoft.EntityFrameworkCore;
using UniversityMVCapp.ViewModels;

var builder = WebApplication.CreateBuilder(args);

string DBconnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(DBconnection));
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultControllerRoute();
app.UseStaticFiles();

app.Run();
