using Microsoft.EntityFrameworkCore;
using Rompecabezas.Logica.Models;
using Rompecabezas.Logica.Servicios;
using SignalR.Web.Hubs;

namespace Rompecabezas.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ISalaService,SalaServiceImpl>();
            builder.Services.AddDbContext<RompeCabezaPw3Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RompeCabezaPw3Context")));
            builder.Services.AddSignalR();

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
            app.MapHub<PuzzleHub>("/puzzleHub");

            app.Run();
        }
    }
}