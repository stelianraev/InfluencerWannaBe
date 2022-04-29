namespace InfluencerWannaBe.Services.Influencers
{
    using System.Linq;
    using System.Collections.Generic;

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

        public Influencer GetInfluencer(string userId)
         => this.data
                .Influencers
                .FirstOrDefault(x => x.UserId == userId);

        public Influencer GetInfluencer(int id)
         => this.data
                .Influencers
                .FirstOrDefault(x => x.Id == id);


        public InfluencerOffers GetInfluencerOffer(Influencer influencer)
        => this.data.InfleuncerOffers.FirstOrDefault(x => x.InfluencerId == influencer.Id);

        public IEnumerable<Offer> InfluencerOffers(InfluencerOffers influencerOffers)
        => this.data.Offers.Where(x => x.SignUpInfluencers.Contains(influencerOffers)).ToList();

        public InfluencerOffers InfluencerOfferInflIdOfferId(int influencerId, int offerId)
            => this.data.InfleuncerOffers.FirstOrDefault(x => x.Influencer.Id == influencerId && x.Offer.Id == offerId);
    }
}
