namespace InfluencerWannaBe.Services.Offers
{
    using InfluencerWannaBe.Data.Models;

    public interface IOfferService
    {
        Offer GetOffer(int id);

        void AddOfferToInfluencer(int id, Offer offer);
    }
}
