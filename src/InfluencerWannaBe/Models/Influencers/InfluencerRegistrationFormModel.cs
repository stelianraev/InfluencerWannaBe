namespace InfluencerWannaBe.Models.Influencers
{
    using System.Collections.Generic;

    public class InfluencerRegistrationFormModel
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public int Age { get; init; }
        public string PhoneNumber { get; init; }
        public string Username { get; init; }
        public int GenderId { get; init; }      
        public string FacebookUrl { get; init; }    
        public string InstagramUrl { get; init; }      
        public string TikTokUrl { get; init; }        
        public string YouTubeUrl { get; init; }
        public string TwitterUrl { get; init; }
        public string WebSiteUrl { get; init; }
        public string Description { get; init; }
        public byte Photo { get; init; }
        public string Discryption { get; init; }
        public string Email { get; init; }
        public int CountryId { get; init; }
        public IEnumerable<InfluencerCountryViewModel> Conutries { get; set; }
        public IEnumerable<InfluencerGenderViewModel> Genders { get; set; }
    }
}
