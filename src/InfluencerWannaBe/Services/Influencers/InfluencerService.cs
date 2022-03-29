namespace InfluencerWannaBe.Services.Influencers
{
    using System.Collections.Generic;
    using System.Linq;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;

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

        public Influencer GetInfluencer(string id)
            => this.data
                   .Influencers
                   .FirstOrDefault(x => x.UserId == id);

        public IEnumerable<Offer> InfluencerOffers(Influencer influencer)
        => this.data.Offers.Where(x => x.SignUpInfluencers.Contains(influencer)).ToList();
    }
}
