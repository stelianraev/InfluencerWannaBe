using InfluencerWannaBe.Models;
using System.Collections.Generic;

namespace InfluencerWannaBe.Services
{
    public interface IGetCollection
    {
        IEnumerable<CountryViewModel> GetCountries();
        IEnumerable<GenderViewModel> GetGender();
    }
}
