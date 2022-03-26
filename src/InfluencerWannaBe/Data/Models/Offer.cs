﻿namespace InfluencerWannaBe.Data.Models
{
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
            
        public int CountryId { get; init; }
        public Country Country { get; init; }

        public byte[] Photo { get; set; }
        public string OwnerId { get; set; }
        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public bool IsPossibleToSignIn { get; set; }

        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();
        public IEnumerable<Influencer> SignUpInfluencers { get; set; } = new List<Influencer>();
    }
}
