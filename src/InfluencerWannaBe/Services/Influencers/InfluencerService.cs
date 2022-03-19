namespace InfluencerWannaBe.Services.Influencers
{
    using InfluencerWannaBe.Data;
    using System;
    using System.Linq;

    public class InfluencerService : IInfluencerService
    {
        private readonly InfluencerWannaBeDbContext data;

        public InfluencerService(InfluencerWannaBeDbContext data)
            => this.data = data;

        public bool IsInfluencer(string userId)
        => this.data
            .Influencers
            .Any(x => x.UserId == userId);

        public int IdByUser(string userId)
         => this.data
                .Influencers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

    }
}
