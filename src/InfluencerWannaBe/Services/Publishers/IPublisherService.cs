namespace InfluencerWannaBe.Services.Publisher
{
    public interface IPublisherService
    {
        bool IsPublisher(string userId);

        public int IdByUser(string userId);
    }
}
