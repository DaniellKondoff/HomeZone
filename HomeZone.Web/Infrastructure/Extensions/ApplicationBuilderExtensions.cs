using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Web.Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace HomeZone.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<HomeZoneDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                Task.Run(async () =>
                {
                    string adminName = WebConstants.AdministratingRole;

                    bool roleExist = await roleManager.RoleExistsAsync(adminName);

                    if (!roleExist)
                    {
                        var role = new IdentityRole { Name = adminName };

                        await roleManager.CreateAsync(role);
                    }

                    var adminEmail = WebConstants.AdministratingEmail;
                    var adminExist = await userManager.FindByEmailAsync(adminEmail);

                    if (adminExist == null)
                    {
                        var adminUser = new User
                        {
                            Email = adminEmail,
                            UserName = adminName,
                            FirstName = adminName,
                            LastName = adminName
                        };

                        await userManager.CreateAsync(adminUser, WebConstants.AdministratingPassword);

                        await userManager.AddToRoleAsync(adminUser, adminName);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
