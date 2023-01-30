using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Json;
using TaskManagement.Controllers;
using TaskManagement.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        /*   var host = new HostBuilder()
                       .ConfigureServices((hostContext, services) =>
                       {
                           services.AddDbContext<TaskManagementDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
                       })
                       .Build();*/

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
        app.UseDeveloperExceptionPage();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();

            // Add a new endpoint for the dropdown data route
            /*
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Tasks}/{action=Create}/{id?}");

            endpoints.MapControllerRoute("default", "{controller=Tasks}/{action=ViewTasks}/{id?}");
            */


        });

        app.Run();
    }
}