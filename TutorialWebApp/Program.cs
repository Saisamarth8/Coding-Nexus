using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using TutorialWebApp.Data;
using TutorialWebApp.Repositories;

namespace TutorialWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MySqlConn");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MySqlConnAuth");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddScoped<ItagRepository, TagRepository>();
            builder.Services.AddScoped<ITutorialPostRepository, TutorialPostRepository>();
            builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
            builder.Services.AddScoped<ITutorialPostLikeRepository, TutorialPostLikeRepository>();
            builder.Services.AddScoped<ITutorialPostCommentRepository, TutorialPostCommentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}