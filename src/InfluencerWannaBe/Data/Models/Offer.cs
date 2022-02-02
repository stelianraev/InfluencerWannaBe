namespace InfluencerWannaBe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Offer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.OfferTitlemaxLenght)]
        public string Title { get; set; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [MaxLength(DataConstants.RequirementsMaxLenght)]
        public string Requirents { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public IEnumerable<Influencer> Influencers { get; set; } = new List<Influencer>();
    }
}
