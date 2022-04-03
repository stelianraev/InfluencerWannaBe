namespace InfluencerWannaBe.Models.Offers
{
    using System.Collections.Generic;
    using InfluencerWannaBe.Data.Models;

    public class OfferViewModel
    {
            public int Id { get; init; }
            public string Title { get; init; }          
            public string Description { get; init; }
            public byte[] Photo { get; init; }
            public int CountryId { get; init; }
            public string CountryName { get; init; }
            public string OwnerId { get; init; }
            public double Payment { get; init; }        
            public bool IsPossibleToSignIn { get; init; }
            public string Requirements { get; init; }
            public ICollection<Review> Reviews { get; init; } = new List<Review>();     
    }
}
