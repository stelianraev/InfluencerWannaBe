namespace InfluencerWannaBeUnitTest.Mocks
{
    using System.Collections.Generic;

    using Moq;
    using InfluencerWannaBe.Models;
    using InfluencerWannaBe.Services;

    public static class GetCollectionMock
    {
        public static IGetCollection Instance
        {       
            get
            {
                var countries = new List<CountryViewModel>()
                {
                    new CountryViewModel{Id = 1, Name = "Bulgaria"},
                    new CountryViewModel{Id = 2, Name = "Spain"},
                    new CountryViewModel{Id = 3, Name = "Romania"},
                    new CountryViewModel{Id = 4, Name = "Italy"},
                    new CountryViewModel{Id = 5, Name = "Germany"},
                    new CountryViewModel{Id = 6, Name = "France"},
                };

                var genders = new List<GenderViewModel>()
                {
                    new GenderViewModel{Id = 1, Name = "Man"},
                    new GenderViewModel{Id = 2, Name = "Woman"},                   
                };

                var mock = new Mock<IGetCollection>();
                mock.Setup(x => x.GetCountries()).Returns(countries);
                mock.Setup(x => x.GetGender()).Returns(genders);

                return mock.Object;
            }
        }
    }
}
