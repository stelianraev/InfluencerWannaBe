namespace InfluencerWannaBe.Services.Offers
{
    using System.Linq;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;

    public class OfferService : IOfferService
    {
        private readonly InfluencerWannaBeDbContext data;

        public OfferService(InfluencerWannaBeDbContext data)
            => this.data = data;

        public void AddOfferToInfluencer(int id, Offer offer)
        => this.data.Influencers.FirstOrDefault(x => x.Id == id).SignUpOffers.Add(offer);

        public Offer GetOffer(int id)
        => this.data.Offers.FirstOrDefault(x => x.Id == id);
    }
}
