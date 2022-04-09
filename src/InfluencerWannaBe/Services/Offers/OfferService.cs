namespace InfluencerWannaBe.Services.Offers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Models.Offers;
    using InfluencerWannaBe.Services.Influencers;

    public class OfferService : IOfferService
    {
        private readonly InfluencerWannaBeDbContext data;
        private readonly IInfluencerService influencerService;

        public OfferService(InfluencerWannaBeDbContext data, IInfluencerService influencerService)
        {
            this.data = data;
            this.influencerService = influencerService;
        }

        public void AddOfferToInfluencer(int id, InfluencerOffers offer)
        => this.data.Influencers.FirstOrDefault(x => x.Id == id).SignUpOffers.Add(offer);

        public ICollection<OffersListingViewModel> OffersByUser(string id)
        => this.data.Offers.Where(x => x.OwnerId == id).Select(x => new OffersListingViewModel
        {
            Id = x.Id,
            Title = x.Title,
            Payment = x.Payment,
            Country = x.Country.Name,            
            Photo = x.Photo,
            PublisherId = x.PublisherId,
            Influencers = x.SignUpInfluencers,
            CreationDate = x.CreationDate,
            Update = x.Update,
            IsExpired = x.IsExpired
        })
            .ToList();

        public ICollection<OffersListingViewModel> OffersBySignInInfluencer(string id)
        {
            var influencer = this.influencerService.GetInfluencer(id);

            var inflOffers = this.data.InfleuncerOffers.Where(x => x.InfluencerId == influencer.Id).ToList();

           return this.data.InfleuncerOffers                 
                .Where(i => i.InfluencerId == influencer.Id)
                .Select(x => new OffersListingViewModel
          {
              Id = x.Id,
              OfferId = x.OfferId,
              Title = x.Offer.Title,
              Payment = x.Offer.Payment,
              Country = x.Offer.Country.Name,
              Photo = x.Offer.Photo,
              PublisherId = x.Offer.PublisherId,
              OwnerId = x.Offer.OwnerId,
              Influencers = x.Offer.SignUpInfluencers
          })
           .ToList();
        }

        public void DeleteOfferById(int id)
        {
           var offer = this.data.Offers.Where(x => x.Id == id).FirstOrDefault();
           this.data.Offers.Remove(offer);
           Task.Run( async() => await this.data.SaveChangesAsync()).GetAwaiter().GetResult();
        }

        public Offer GetOffer(int id)
        => this.data.Offers.FirstOrDefault(x => x.Id == id);
    }
}
