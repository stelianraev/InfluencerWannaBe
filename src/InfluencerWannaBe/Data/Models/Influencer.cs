namespace InfluencerWannaBe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Influencer
    {
        public int Id { get; init; }
        
        [Required]
        [MaxLength(DataConstants.FirstNameMaxLenght)]
        public string FirstName { get; init; }

        [MaxLength(DataConstants.MiddleName)]
        public string MiddleName { get; init; }

        [MaxLength(DataConstants.LastName)]
        public string LastName { get; init; }

        [Range(0, 20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(DataConstants.Username)]
        public string Username { get; set; }

        [Range(0, 120)]
        public int Age { get; init; }
                
        public int Genderid { get; set; }
        public Gender Gender { get; set; }

        [Url]
        public string FacebookUrl { get; set; }
        [Url]
        public string WebSiteUrl { get; set; }
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
        public string Description { get; set; }

        [Required]
        public byte Photo { get; set; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Discription { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public List<Offer> Offers { get; init; } = new List<Offer>();
        public List<Review> Reviews { get; init; } = new List<Review>();
        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
    }
}
