namespace InfluencerWannaBe.Models.Offers
{
    using InfluencerWannaBe.Data.Models;
    using System.Collections.Generic;
    public class OfferViewModel
    {
            public int Id { get; init; }
            public string Title { get; init; }          
            public string Description { get; set; }
            public byte[] Photo { get; set; }
            public int CountryId { get; set; }
            public string CountryName { get; init; }
            public string OwnerId { get; set; }
            public bool IsPossibleToSignIn { get; set; }
            public string Requirements { get; set; }
            public ICollection<Review> Reviews { get; init; } = new List<Review>();     
    }
}
