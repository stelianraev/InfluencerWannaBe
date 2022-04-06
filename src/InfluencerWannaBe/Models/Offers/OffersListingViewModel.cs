using InfluencerWannaBe.Data.Models;
using System.Collections.Generic;

namespace InfluencerWannaBe.Models.Offers
{
    public class OffersListingViewModel
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public int PublisherId { get; init; }
        public string PublisherUserName { get; init; }
        public double Payment { get; init; }
        public string Country { get; init; }
        public byte[] Photo { get; init; }
        public string OwnerId { get; init; }
        public ICollection<InfluencerOffers> Influencers { get; init; }
    }
}
