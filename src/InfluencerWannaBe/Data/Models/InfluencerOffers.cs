namespace InfluencerWannaBe.Data.Models
{
    public class InfluencerOffers
    {
        public int Id { get; init; }
        public int InfluencerId{ get; init; }
        public Influencer Influencer { get; set; }

        public int OfferId { get; init; }
        public Offer Offer { get; set; }
    }
}
