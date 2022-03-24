namespace InfluencerWannaBe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Country
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.CountryNameMaxLenght)]
        public string Name { get; set; }

        public IEnumerable<Influencer> Influencers{get; set;} = new List<Influencer>();
        public IEnumerable<Publisher> Publishers{get; set;} = new List<Publisher>();
    }
}
