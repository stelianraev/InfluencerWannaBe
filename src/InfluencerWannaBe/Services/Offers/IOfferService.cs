namespace InfluencerWannaBe.Services.Offers
{
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Models.Offers;
    using System.Collections.Generic;

    public interface IOfferService
    {
        Offer GetOffer(int id);

        void AddOfferToInfluencer(int id, InfluencerOffers offer);
        public void DeleteOfferById(int id);

        public ICollection<OffersListingViewModel> OffersByUser(string id);

        public ICollection<OffersListingViewModel> OffersBySignInInfluencer(string id);

    }
}
