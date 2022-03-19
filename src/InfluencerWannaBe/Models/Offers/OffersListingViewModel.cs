namespace InfluencerWannaBe.Models.Offers
{
    using InfluencerWannaBe.Data.Models;

    public class OffersListingViewModel
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string PublisherUserName { get; init; }
        public double Payment { get; init; }
        public byte[] Photo { get; init; }
    }
}
