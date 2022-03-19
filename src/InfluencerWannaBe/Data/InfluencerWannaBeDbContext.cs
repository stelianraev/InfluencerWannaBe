namespace InfluencerWannaBe.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using InfluencerWannaBe.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class InfluencerWannaBeDbContext : IdentityDbContext
    {
        public InfluencerWannaBeDbContext(DbContextOptions<InfluencerWannaBeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Influencer> Influencers { get; init; }
        public DbSet<Publisher> Publishers { get; init; }
        public DbSet<Country> Countries { get; init; }
        public DbSet<Offer> Offers { get; init; }
        public DbSet<Review> Reviews { get; init; }
        public DbSet<Gender> Genders { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Gender>()
                .HasMany(x => x.Influencers)
                .WithOne(x => x.Gender)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Country>()
                .HasMany(x => x.Influencers)
                .WithOne(x => x.Country)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Publisher>()
                .HasMany(i => i.Offers)
                .WithOne(c => c.Publisher)
                .HasForeignKey(x => x.PublisherId);

            builder
                .Entity<Influencer>()
                .HasMany(i => i.Offers)
                .WithMany(o => o.Influencers)
                .UsingEntity(x => x.ToTable("InfluencerOffers"));

            builder
                .Entity<Influencer>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Influencer)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Influencer>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Influencer>(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base
                .OnModelCreating(builder);
        }
    }
}
