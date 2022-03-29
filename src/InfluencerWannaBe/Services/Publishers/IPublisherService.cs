namespace InfluencerWannaBe.Services.Publisher
{
    using System.Collections.Generic;
    using InfluencerWannaBe.Data.Models;

    public interface IPublisherService
    {
        bool IsPublisher(string userId);

        public int IdByUser(string userId);

        public Data.Models.Publisher GetPublisher(string Id);

        public IEnumerable<Offer> GetPublisherOffers(int id);
    }
}
