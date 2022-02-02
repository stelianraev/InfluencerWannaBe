namespace InfluencerWannaBe.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using InfluencerWannaBe.Data.Models;

    public class InfluencerWannaBeDbContext : IdentityDbContext
    {
        public InfluencerWannaBeDbContext(DbContextOptions<InfluencerWannaBeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Influencer> Influencers { get; init; }
        public DbSet<Company> Companies { get; init; }
        public DbSet<Country> Countries { get; init; }
        public DbSet<Offer> Offers { get; init; }
        public DbSet<Review> Reviews { get; init; }
        public DbSet<Gender> Genders { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Gender>()
                .HasMany(x => x.Influencers)
                .WithOne(x => x.Gender)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Country>()
                .HasMany(x => x.Influencers)
                .WithOne(x => x.Country)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Influencer>()
                .HasMany(i => i.Companies)
                .WithMany(c => c.Influencers)
                .UsingEntity(x => x.ToTable("InfluencerCompanies"));

            builder.Entity<Influencer>()
                .HasMany(i => i.Offers)
                .WithMany(o => o.Influencers)
                .UsingEntity(x => x.ToTable("InfluencerOffers"));

            builder.Entity<Influencer>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Influencer)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
