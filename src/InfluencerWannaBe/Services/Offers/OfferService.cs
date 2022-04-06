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
            Influencers = x.SignUpInfluencers
        })
            .ToList();

        public ICollection<OffersListingViewModel> OffersBySignInInfluencer(string id)
        {
            var influencer = this.influencerService.GetInfluencer(id);

            var inflOffer = this.data.InfleuncerOffers.FirstOrDefault(x => x.InfluencerId == influencer.Id);

           return this.data.Offers
                .Where(i => i.SignUpInfluencers.Contains(inflOffer))
                .Select(x => new OffersListingViewModel
          {
              Id = x.Id,
              Title = x.Title,
              Payment = x.Payment,
              Country = x.Country.Name,
              Photo = x.Photo,
              PublisherId = x.PublisherId,
              OwnerId = x.OwnerId,
              Influencers = x.SignUpInfluencers
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
