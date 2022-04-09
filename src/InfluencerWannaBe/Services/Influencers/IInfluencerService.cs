using InfluencerWannaBe.Data.Models;
using System.Collections.Generic;

namespace InfluencerWannaBe.Services.Influencers
{
    public interface IInfluencerService
    {
        bool IsInfluencer(string userId);

        Influencer GetInfluencer(string id);

        int IdByUser(string userId);

        InfluencerOffers GetInfluencerOffer(Influencer influencer);

        InfluencerOffers InfluencerOfferInflIdOfferId(int influencerId, int offerId);

        IEnumerable<Offer> InfluencerOffers(InfluencerOffers influencerOffers);
    }
}