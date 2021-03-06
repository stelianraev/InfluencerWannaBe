namespace InfluencerWannaBe
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Infrastructure;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBe.Services.Publisher;
    using InfluencerWannaBe.Services.Influencers;

    public class Startup
    {
        public Startup(IConfiguration configuration)       
        => Configuration = configuration;       

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IPublisherService, PublisherService>()
                .AddTransient<IEmailSender, EmailSender>()
                .AddTransient<IInfluencerService, InfluencerService>()
                .AddTransient<IGetCollection, GetCollection>()
                .AddTransient<IOfferService, OfferService>()
                .AddDbContext<InfluencerWannaBeDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMemoryCache();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })                
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<InfluencerWannaBeDbContext>();            

            services.AddControllersWithViews( options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
