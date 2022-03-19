namespace InfluencerWannaBe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Offer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.OfferTitleMaxLenght)]
        public string Title { get; set; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(DataConstants.RequirementsMaxLenght)]
        [MinLength(DataConstants.RequirementsMinLenght)]
        public string Requirents { get; set; }

        [Range(0, double.MaxValue)]
        public double Payment { get; set; }

        public int PublisherId { get; set; }

        public byte[] Photo { get; set; }

        public Publisher Publisher { get; set; }

        public bool IsPossibleToSignIn { get; set; }

        public IEnumerable<Influencer> Influencers { get; set; } = new List<Influencer>();
    }
}
