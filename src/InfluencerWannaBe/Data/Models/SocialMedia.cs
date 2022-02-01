namespace InfluencerWannaBe.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SocialMedia
    {
        public int Id { get; init; }

        [Url]
        public string FacebookUrl { get; set; }
        [Url]
        public string InstagramUrl { get; set; }
        [Url]
        public string TikTokUrl { get; set; }
        [Url]
        public string YouTubeUrl { get; set; }

        //if the you have more profiles in different platforms tobe possible to describe it
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

    }
}
