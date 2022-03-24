using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfluencerWannaBe.Data.Models
{
    public class Publisher
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.FirstNameMaxLenght)]
        public string FirstName { get; set; }

        [MaxLength(DataConstants.MiddleNameMaxLenght)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(DataConstants.LastNameMaxLenght)]
        public string LastName { get; set; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(DataConstants.UsernameMaxLenght)]
        public string Username { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
                
        public string UserId { get; set; }

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

        public byte[] Photo { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        [MaxLength(DataConstants.PhoneNumberMaxLenght)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //if the you have more profiles in different platforms tobe possible to describe it
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string AnotherLinks { get; set; }
        public List<Offer> Offers { get; init; } = new List<Offer>();
        public List<Review> Reviews { get; init; } = new List<Review>();
    }
}
