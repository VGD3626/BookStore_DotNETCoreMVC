using BookStore_MVC.Claims;
using BookStore_MVC.Data;
using BookStore_MVC.Models;
using BookStore_MVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace BookStore_MVC
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the database context
            services.AddDbContextPool<BookStoreDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("BookStoreDb")));

            // Configure Identity services
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<BookStoreDbContext>()
            .AddDefaultTokenProviders();

            // Register application services
            services.AddScoped<IBookRepository, SqlBookRepository>();
            services.AddScoped<ILanguageRepository, SqlLanguageRepository>();
            services.AddScoped<ICategoryRepository, SqlCategoryRepository>();
            services.AddScoped<IUserRepository, SqlUserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipalFactory>();
            services.AddScoped<IOrderService, OrderService>();

            // Configure session and distributed memory cache
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Configure MVC with JSON settings to ignore reference loops
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Enable AutoMapper for dependency injection
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Configure the application cookie settings
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/login";
            });

            // Add Razor runtime compilation in development environment
            if (_env.IsDevelopment())
            {
                services.AddRazorPages().AddRazorRuntimeCompilation();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession(); // Enable session
            app.UseAuthentication();
            app.UseAuthorization();

            // Seed roles and admin user on startup
            SeedRoles(serviceProvider).Wait();
            CreateAdminUser(serviceProvider).Wait();

            // Configure endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Seed roles if they do not exist
        private async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "ADMIN", "NORMAL USER" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        // Create admin user if not present
        private async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            var existingUser = await userManager.FindByEmailAsync(adminEmail);
            if (existingUser == null)
            {
                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    DateOfBirth = DateTime.UtcNow.AddYears(-30)
                };

                var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "ADMIN");
                }
            }
        }
    }
}
