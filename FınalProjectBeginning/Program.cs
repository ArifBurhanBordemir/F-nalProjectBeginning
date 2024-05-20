using FinalProjectBeginning.Data;
using FınalProjectBeginning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FınalProjectBeginning
{
    public class Program
    {
        public static async /*void*/ Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<CetUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireAdministratorRole",
            //         policy => policy.RequireRole("Restoran"));
            //});

            //builder.Services.AddScoped<KulsayfasiViewComponent>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();






            using(var scope = app.Services.CreateScope())
            {
                var roleManager = 
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Restoran", "Member" };

                foreach(var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                            
                }

            }
            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<CetUser>>();
                string email = "admin@gmail.com";
                string password = "Test1234.";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new CetUser();
                    user.Email = email;
                    user.Name = email;
                    user.Surname = email;
                    user.BirthDate = DateTime.Now;
                    user.EmailConfirmed = true;
                    user.UserName = email;
                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }


            }



            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<CetUser>>();
                string email = "restoran@gmail.com";
                string password = "Test1234.";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new CetUser();
                    user.Email = email;
                    user.Name = email;
                    user.Surname = email;
                    user.BirthDate = DateTime.Now;
                    user.EmailConfirmed = true;
                    user.UserName = email;
                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Restoran");
                }


            }













            app.Run();





            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });
        }
    }
}
