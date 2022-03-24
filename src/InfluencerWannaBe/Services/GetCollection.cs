namespace InfluencerWannaBe.Services
{
    using System.Linq;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Models;
    using System.Collections.Generic;
    public class GetCollection : IGetCollection
    {
        private readonly InfluencerWannaBeDbContext data;
        public GetCollection(InfluencerWannaBeDbContext data)
        {
            this.data = data;
        }
        public IEnumerable<CountryViewModel> GetCountries()
        => this.data
            .Countries
            .Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        public IEnumerable<GenderViewModel> GetGender()
        => this.data
           .Genders
           .Select(c => new GenderViewModel
           {
               Id = c.Id,
               Name = c.Name
           })
           .ToList();
    }
}
