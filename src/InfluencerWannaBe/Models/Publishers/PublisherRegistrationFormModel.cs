namespace InfluencerWannaBe.Models.Publishers
{
    using InfluencerWannaBe.Data;
    using System.Collections.Generic;
    using InfluencerWannaBe.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class PublisherRegistrationFormModel
    {
        [Required]
        [StringLength(DataConstants.FirstNameMaxLenght, MinimumLength = DataConstants.FirstNameMinLenght, ErrorMessage = "First name should be between {2} and {1}")]
        public string FirstName { get; init; }

        [StringLength(DataConstants.MiddleNameMaxLenght, ErrorMessage = "Middle name maximum is {1}")]
        public string MiddleName { get; init; }

        [Required]
        [StringLength(DataConstants.LastNameMaxLenght, MinimumLength = DataConstants.LastNameMinLenght, ErrorMessage = "Last name should be between {2} and {1}")]
        public string LastName { get; init; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(DataConstants.UsernameMaxLenght)]
        public string Username { get; set; }

        public string UserId { get; set; }

        [Url]
        public string WebSiteUrl { get; set; }
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

        public int GenderId { get; init; }

        //[Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [MaxLength(DataConstants.PhoneNumberMaxLenght)]
        [StringLength(DataConstants.PhoneNumberMaxLenght, ErrorMessage = "Phone nomer maximum is {1}")]
        public string PhoneNumber { get; init; }

        //if the you have more profiles in different platforms to be possible to describe it
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string AnotherLinks { get; set; }
        public int CountryId { get; set; }
        public IEnumerable<CountryViewModel> Conutries { get; set; }
        public IEnumerable<Offer> Offers { get; init; } = new List<Offer>();
        public IEnumerable<Review> Reviews { get; init; } = new List<Review>();
        public IEnumerable<GenderViewModel> Genders { get; set; }
    }
}
