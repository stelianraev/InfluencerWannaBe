namespace InfluencerWannaBe.Models.Influencers
{
    public class InfluencerListingViewModel
    {
        public int Id { get; init; }

        public string Username { get; init; }

        public string Facebook { get; init; }
        public string Instagram { get; init; }
        public byte[] Photo { get; init; }
        public bool? AcceptedForTheOffer { get; init; }
    }
}
