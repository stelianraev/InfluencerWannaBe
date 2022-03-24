namespace InfluencerWannaBe.Models.Offers
{
    using InfluencerWannaBe.Data;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OffersRegistrationFormModel
    {
        [Required]
        [StringLength(DataConstants.OfferTitleMaxLenght, MinimumLength = DataConstants.OfferTitleMinLenght, ErrorMessage = "The title should be between {2} and {1}")]
        public string Title { get; set; }

        [MaxLength(DataConstants.OfferDescriptionMaxLengt)]
        public string Description { get; set; }

        [Required]
        [StringLength(DataConstants.OfferRequirementsMaxLenght, MinimumLength = DataConstants.OfferRequirementsMinLenght, ErrorMessage = "Requerements should be between {2} and {1}")]
        public string Requirements { get; set; }

        [Range(0, double.MaxValue)]
        public double Payment { get; set; }

        public int OwnerId { get; set; }

        public byte[] Photo { get; set; }

        public bool IsPossibleToSignIn { get; set; }
        public int CountryId { get; init; }
        public IEnumerable<CountryViewModel> Conutries { get; set; }
    }
}
