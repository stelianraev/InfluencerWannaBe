namespace InfluencerWannaBe.Services.Publisher
{
    using System.Collections.Generic;
    using System.Linq;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;

    public class PublisherService : IPublisherService
    {
        private readonly InfluencerWannaBeDbContext data;

        public PublisherService(InfluencerWannaBeDbContext data)
            => this.data = data;

        public bool IsPublisher(string userId)
        => this.data
            .Publishers
            .Any(x => x.UserId == userId);

        public int IdByUser(string userId)
         => this.data
                .Publishers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();                  

        public Publisher GetPublisher(string id)
         => this.data.Publishers.FirstOrDefault(x => x.UserId == id);

        public IEnumerable<Offer> GetPublisherOffers(int id)
        => this.data.Offers.Where(x => x.PublisherId == id).ToList();
    }
}
