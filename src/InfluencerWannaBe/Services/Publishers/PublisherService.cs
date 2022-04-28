namespace InfluencerWannaBe.Services.Publisher
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public Publisher GetPublisher(int id)
        => this.data.Publishers.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Offer> GetPublisherOffers(int id)
        => this.data.Offers.Where(x => x.PublisherId == id).ToList();

        public void DeletePublisherById(int id)
        {
            var publisher = this.data.Publishers.Where(x => x.Id == id).FirstOrDefault();
            this.data.Publishers.Remove(publisher);
            Task.Run(async () => await this.data.SaveChangesAsync()).GetAwaiter().GetResult();
        }
    }
}
