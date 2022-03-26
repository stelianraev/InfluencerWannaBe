using InfluencerWannaBe.Data.Models;
using System.Collections.Generic;

namespace InfluencerWannaBe.Models.Publishers
{
    public class PublisherViewModel
    {
            public string FirstName { get; init; }
            public string MiddleName { get; init; }
            public string LastName { get; init; }
            public string PhoneNumber { get; set; }
            public string Username { get; set; }
            public string Gender { get; set; }
            public string FacebookUrl { get; set; }
            public string WebSiteUrl { get; set; }
            public string InstagramUrl { get; set; }
            public string TikTokUrl { get; set; }
            public string YouTubeUrl { get; set; }
            public string TwitterUrl { get; set; }
            //if the you have more profiles in different platforms tobe possible to describe it
            public string Description { get; set; }
            public byte[] Photo { get; set; }
            public int CountryId { get; set; }
            public string CountryName { get; init; }
            public string Email { get; set; }
            public ICollection<Offer> Offers { get; init; } = new List<Offer>();
            public ICollection<Review> Reviews { get; init; } = new List<Review>();     
    }
}
