namespace InfluencerWannaBe.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; init; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; init; }

        [Required]
        public int StarCount { get; init; }
    }
}
