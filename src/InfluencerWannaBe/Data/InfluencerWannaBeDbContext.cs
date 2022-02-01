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
        public DbSet<SocialMedia> SocialMedias { get; init; }
    }
}
