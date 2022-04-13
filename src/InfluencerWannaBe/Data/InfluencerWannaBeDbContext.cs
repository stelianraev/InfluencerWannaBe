namespace InfluencerWannaBe.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using InfluencerWannaBe.Data.Models;

    public class InfluencerWannaBeDbContext : IdentityDbContext<User>
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
        public DbSet<InfluencerOffers> InfleuncerOffers {get; init;}

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
                .Entity<Country>()
                .HasMany(x => x.Publishers)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Publisher>()
                .HasMany(i => i.Offers)
                .WithOne(c => c.Publisher)
                .HasForeignKey(x => x.PublisherId);

            builder
                .Entity<Publisher>()
                .HasMany(i => i.Reviews)
                .WithOne(c => c.Publisher)
                .HasForeignKey(x => x.PublisherId);

            builder
                .Entity<InfluencerOffers>()
                .HasOne(x => x.Influencer)
                .WithMany(o => o.SignUpOffers)
                .HasForeignKey(x => x.InfluencerId);

            builder
               .Entity<InfluencerOffers>()
               .HasOne(x => x.Offer)
               .WithMany(i => i.SignUpInfluencers)
               .HasForeignKey(x => x.OfferId);

            builder
                .Entity<Influencer>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Influencer)
                .OnDelete(DeleteBehavior.Restrict);

            base
                .OnModelCreating(builder);
        }
    }
}
