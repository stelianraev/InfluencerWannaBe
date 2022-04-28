namespace InfluencerWannaBe.Models.Influencers
{
    using InfluencerWannaBe.Data;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InfluencerRegistrationFormModel
    {
        [Required]
        [StringLength(DataConstants.FirstNameMaxLenght, MinimumLength = DataConstants.FirstNameMinLenght, ErrorMessage = "First name should be between {2} and {1}")]
        public string FirstName { get; init; }

        [StringLength(DataConstants.MiddleNameMaxLenght, ErrorMessage = "Middle name maximum is {1}")]
        public string MiddleName { get; init; }

        [Required]
        [StringLength(DataConstants.LastNameMaxLenght, MinimumLength = DataConstants.LastNameMinLenght, ErrorMessage = "Last name should be between {2} and {1}")]
        public string LastName { get; init; }

        [Range(0, 120)]
        public int Age { get; init; }

        [MaxLength(DataConstants.PhoneNumberMaxLenght)]
        [StringLength(DataConstants.PhoneNumberMaxLenght, ErrorMessage = "Phone nomer maximum is {1}")]
        public string PhoneNumber { get; init; }

        [Required]
        [StringLength(DataConstants.UsernameMaxLenght, MinimumLength = DataConstants.UsernameMinLenght, ErrorMessage = "Username should be between {2} and {1}")]
        public string Username { get; init; }
        public int GenderId { get; init; }

        [Url(ErrorMessage = "Invalid url")]
        public string FacebookUrl { get; init; }

        [Url(ErrorMessage = "Invalid url")]
        public string InstagramUrl { get; init; }

        [Url(ErrorMessage = "Invalid url")]
        public string TikTokUrl { get; init; }

        [Url(ErrorMessage = "Invalid url")]
        public string YouTubeUrl { get; init; }

        [Url(ErrorMessage = "Invalid url")]
        public string TwitterUrl { get; init; }

        [Url(ErrorMessage = "Invalid url")]
        public string WebSiteUrl { get; init; }

        [StringLength(DataConstants.DescriptionMaxLength, ErrorMessage = "Description maximum is {1}")]
        public string Description { get; init; }
        public byte Photo { get; init; }

        //[Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public int CountryId { get; init; }
        public ICollection<CountryViewModel> Conutries { get; set; }
        public ICollection<GenderViewModel> Genders { get; set; }
    }
}
