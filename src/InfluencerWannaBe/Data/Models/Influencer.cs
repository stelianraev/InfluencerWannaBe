namespace InfluencerWannaBe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Influencer
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(DataConstants.FirstNameMaxLenght)]
        public string FirstName { get; init; }

        [MaxLength(DataConstants.MiddleNameMaxLenght)]
        public string MiddleName { get; init; }

        [Required]
        [MaxLength(DataConstants.LastNameMaxLenght)]
        public string LastName { get; init; }

        [MaxLength(DataConstants.PhoneNumberMaxLenght)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(DataConstants.UsernameMaxLenght)]
        public string Username { get; set; }

        [Range(0, 120)]
        public int Age { get; init; }
                
        public int GenderId { get; set; }
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
        public byte[] Photo { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<InfluencerOffers> SignUpOffers { get; set; } = new List<InfluencerOffers>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public string UserId { get; set; }
    }
}
