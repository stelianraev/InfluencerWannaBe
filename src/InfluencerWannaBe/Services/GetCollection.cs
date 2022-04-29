namespace InfluencerWannaBe.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Models;
    public class GetCollection : IGetCollection
    {
        private readonly InfluencerWannaBeDbContext data;
        public GetCollection(InfluencerWannaBeDbContext data)
        {
            this.data = data;
        }
        public ICollection<CountryViewModel> GetCountries()
        => this.data
            .Countries
            .Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        public ICollection<GenderViewModel> GetGender()
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
