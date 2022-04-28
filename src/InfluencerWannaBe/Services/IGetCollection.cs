using InfluencerWannaBe.Models;
using System.Collections.Generic;

namespace InfluencerWannaBe.Services
{
    public interface IGetCollection
    {
        ICollection<CountryViewModel> GetCountries();
        ICollection<GenderViewModel> GetGender();
    }
}
