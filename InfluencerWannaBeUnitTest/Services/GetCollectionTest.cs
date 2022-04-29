namespace InfluencerWannaBeUnitTest.Services
{
    using System.Linq;

    using NUnit.Framework;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBeUnitTest.Mocks;

    [TestFixture]
    public class GetCollectionTest
    {
        private InfluencerWannaBeDbContext data;
        private IGetCollection getCollectionService;

        [SetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.getCollectionService = new GetCollection(this.data);
        }

        [Test]
        public void GetCountriesShouldReturnAllCountriesFromDb()
        {
            var result = this.getCollectionService.GetCountries();
            var allCountries = this.data.Countries.Count();

            Assert.AreEqual(allCountries, result.Count());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetGenderShouldReturnAllGendersFromDb()
        {
            var result = this.getCollectionService.GetGender();
            var allGenders = this.data.Genders.Count();

            Assert.AreEqual(allGenders, result.Count());

            this.data.Database.EnsureDeleted();
        }
    }
}
