namespace InfluencerWannaBe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; init; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; init; }

        [Required]
        public int StarCount { get; init; }

        public int InfluencerId { get; set; }
        public Influencer Influencer { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
