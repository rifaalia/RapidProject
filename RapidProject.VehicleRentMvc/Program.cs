using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.DAL.Services;
using RapidProject.VehicleRentMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var projectDb = builder.Configuration.GetConnectionString("projectDB");

builder.Services.AddDbContext<RentVehicleDbContext>(options =>
    options.UseSqlServer(projectDb)
);

builder.Services.AddScoped<IVehicleRepository, VehicleService>();
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeService>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
