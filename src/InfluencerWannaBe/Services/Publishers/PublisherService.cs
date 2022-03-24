namespace InfluencerWannaBe.Services.Publisher
{
    using System.Linq;
    using InfluencerWannaBe.Data;

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
    }
}
