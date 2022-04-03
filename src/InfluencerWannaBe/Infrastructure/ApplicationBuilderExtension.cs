namespace InfluencerWannaBe.Infrastructure
{
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using InfluencerWannaBe.Data;
    using System.IO;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using InfluencerWannaBe.Data.Models;
    using System;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
           using var scopeServices = app.ApplicationServices.CreateScope();

            var data = scopeServices.ServiceProvider.GetService<InfluencerWannaBeDbContext>();

            data.Database.Migrate();

            SeedCountries(data);
            SeedGenders(data);

            return app;
        }

        private static void SeedCountries(InfluencerWannaBeDbContext data)
        {
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

        private static void SeedGenders(InfluencerWannaBeDbContext data)
        {
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
    }
}
