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
        [Url]
        public string FacebookUrl { get; set; }
        [Url]
        public string InstagramUrl { get; set; }
        [Url]
        public string TikTokUrl { get; set; }
        [Url]
        public string YouTubeUrl { get; set; }
        [Url]
        public string TwitterUrl { get; set; }

        //if the you have more profiles in different platforms tobe possible to describe it
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string AnotherLinks { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Influencer> Influencers { get; set; } = new List<Influencer>();
    }
}
