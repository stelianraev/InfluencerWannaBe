namespace InfluencerWannaBe.Services.Offers
{
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Models.Offers;
    using System.Collections.Generic;

    public interface IOfferService
    {
        Offer GetOffer(int id);

        void AddOfferToInfluencer(int id, Offer offer);
        public void DeleteOfferById(int id);

        ICollection<OffersListingViewModel> OffersByUser(string id);

    }
}
