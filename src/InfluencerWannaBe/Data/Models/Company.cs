using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfluencerWannaBe.Data.Models
{
    public class Company
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.CompanyNameMaxLenght)]
        public string Name { get; set; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public Country Country { get; set; }

        [Url]
        public string WebsiteUrl { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public List<Review> Reviews { get; init; } = new List<Review>();
        public List<Influencer> InfluencersWorkedWith { get; init; } = new List<Influencer>();
    }
}
