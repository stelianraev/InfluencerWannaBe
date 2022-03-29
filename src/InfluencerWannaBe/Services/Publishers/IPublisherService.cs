namespace InfluencerWannaBe.Services.Publisher
{
    using System.Collections.Generic;
    using InfluencerWannaBe.Data.Models;

    public interface IPublisherService
    {
        bool IsPublisher(string userId);

        public int IdByUser(string userId);

        public Publisher GetPublisher(string id);
        public Publisher GetPublisher(int id);

        public IEnumerable<Offer> GetPublisherOffers(int id);
    }
}
