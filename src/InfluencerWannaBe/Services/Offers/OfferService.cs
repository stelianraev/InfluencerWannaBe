namespace InfluencerWannaBe.Services.Offers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Models.Offers;

    public class OfferService : IOfferService
    {
        private readonly InfluencerWannaBeDbContext data;

        public OfferService(InfluencerWannaBeDbContext data)
            => this.data = data;

        public void AddOfferToInfluencer(int id, Offer offer)
        => this.data.Influencers.FirstOrDefault(x => x.Id == id).SignUpOffers.Add(offer);

        public ICollection<OffersListingViewModel> OffersByUser(string id)
        => this.data.Offers.Where(x => x.OwnerId == id).Select(x => new OffersListingViewModel
        {
            Id = x.Id,
            Title = x.Title,
            Payment = x.Payment,
            Country = x.Country.Name,            
            Photo = x.Photo,
            PublisherId = x.PublisherId
        })
            .ToList();

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
