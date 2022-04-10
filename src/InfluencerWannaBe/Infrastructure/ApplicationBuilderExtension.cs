namespace InfluencerWannaBe.Infrastructure
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using System;
    using Microsoft.AspNetCore.Identity;

    using static Models.Constants.AdminConstants;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
           using var scopeServices = app.ApplicationServices.CreateScope();
           var services = scopeServices.ServiceProvider;

            var data = scopeServices.ServiceProvider.GetService<InfluencerWannaBeDbContext>();

            MigrateDatabase(services);

            SeedCountries(services);
            SeedGenders(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<InfluencerWannaBeDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCountries(IServiceProvider services)
        {
            var data = services.GetRequiredService<InfluencerWannaBeDbContext>();

            if (data.Countries.Any())
            {
                return;
            }

            var path = Path.GetFullPath("CountriesSeed.json");

            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();

                var test = JsonConvert.DeserializeObject<List<string>>(json);

                var tempCountries = new List<Country>();

                foreach (var country in test)
                {
                    tempCountries.Add(new Country() { Name = country});    
                }

                data.Countries.AddRange(tempCountries);
            }

                data.SaveChanges();
        }

        private static void SeedGenders(IServiceProvider services)
        {
            var data = services.GetRequiredService<InfluencerWannaBeDbContext>();

            if (data.Genders.Any())
            {
                return;
            }

            data.Genders.AddRange(new[]
            {
                new Gender(){Name = "Man"},
                new Gender(){Name = "Woman"},
                new Gender() {Name = "Trans"},
                new Gender() {Name = "Drag"}
            });
       
            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@iwb.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    };

                    var result = await userManager.CreateAsync(user, adminPassword);

                    var roleCreate = await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
