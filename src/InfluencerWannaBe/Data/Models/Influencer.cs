namespace InfluencerWannaBe.Data.Models
{
    using InfluencerWannaBe.Data.Models.Enum;
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

        [Required]
        public Gender Gender { get; set; }
        public int SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; init; }

        [Required]
        public byte Photo { get; set; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Discryption { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public List<Offer> Offers { get; init; } = new List<Offer>();
        public List<Review> Reviews { get; init; } = new List<Review>();
    }
}
