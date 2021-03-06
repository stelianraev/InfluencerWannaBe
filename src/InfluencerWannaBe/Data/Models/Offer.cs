namespace InfluencerWannaBe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Offer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.OfferTitleMaxLenght)]
        public string Title { get; set; }

        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(DataConstants.OfferRequirementsMaxLenght)]
        [MinLength(DataConstants.OfferRequirementsMinLenght)]
        public string Requirents { get; set; }

        [Range(0, double.MaxValue)]
        public double Payment { get; set; }
            
        public int CountryId { get; set; }
        public Country Country { get; init; }

        public byte[] Photo { get; set; }
        public string OwnerId { get; set; }
        public int PublisherId { get; init; }

        public Publisher Publisher { get; init; }

        public bool IsPossibleToSignIn { get; set; }
        public bool IsExpired { get; set; }
        public DateTime CreationDate { get; init; }
        public DateTime Update { get; set; }
        public DateTime ExpireDate { get; set; }
        public ICollection<Publisher> Publishers { get; set; } = new List<Publisher>();
        public ICollection<InfluencerOffers> SignUpInfluencers { get; set; } = new List<InfluencerOffers>();
    }
}
