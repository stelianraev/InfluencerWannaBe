namespace InfluencerWannaBe.Data.Models
{
    using System.Collections.Generic;

    public class Gender
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public IEnumerable<Influencer> Influencers { get; set; } = new List<Influencer>();
    }
}
