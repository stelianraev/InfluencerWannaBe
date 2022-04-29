namespace InfluencerWannaBeUnitTest.Services
{
    using System.Linq;

    using NUnit.Framework;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBeUnitTest.Mocks;
    using InfluencerWannaBe.Services.Publisher;

    [TestFixture]
    public class PublisherServiceTest
    {
        private InfluencerWannaBeDbContext data;
        private IPublisherService publisherService;

        [SetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.publisherService = new PublisherService(this.data);
        }

        [Test]
        public void IsPublisherReturnIsUserPublisherOrNot()
        {
            var isPublisher = this.publisherService.IsPublisher("testovUser");
            var isNotPublisher = this.publisherService.IsPublisher("notPublisher");

            Assert.AreEqual(true, isPublisher);
            Assert.AreEqual(false, isNotPublisher);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void IdByUserReturnPublisherIdFromUserId()
        {
            var result = this.publisherService.IdByUser("testovUser");

            var publisherFromDb = this.data.Publishers.FirstOrDefault(x => x.UserId == "testovUser");

            Assert.AreEqual(publisherFromDb.Id, result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetPublisherReturnsPublisherByUserId()
        {
            var result = this.publisherService.GetPublisher("testovUser");

            var publisherFromDb = this.data.Publishers.FirstOrDefault(x => x.UserId == "testovUser");

            Assert.AreEqual(publisherFromDb.Id, result.Id);
            Assert.AreEqual(publisherFromDb.CountryId, result.CountryId);
            Assert.AreEqual(publisherFromDb.Description, result.Description);
            Assert.AreEqual(publisherFromDb.Email, result.Email);
            Assert.AreEqual(publisherFromDb.FirstName, result.FirstName);
            Assert.AreEqual(publisherFromDb.MiddleName, result.MiddleName);
            Assert.AreEqual(publisherFromDb.LastName, result.LastName);
            Assert.AreEqual(publisherFromDb.UserId, result.UserId);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetPublisherByIdReturnsPublisherById()
        {
            var result = this.publisherService.GetPublisher(2);

            var publisherFromDb = this.data.Publishers.FirstOrDefault(x => x.Id == 2);

            Assert.AreEqual(publisherFromDb.Id, result.Id);
            Assert.AreEqual(publisherFromDb.CountryId, result.CountryId);
            Assert.AreEqual(publisherFromDb.Description, result.Description);
            Assert.AreEqual(publisherFromDb.Email, result.Email);
            Assert.AreEqual(publisherFromDb.FirstName, result.FirstName);
            Assert.AreEqual(publisherFromDb.MiddleName, result.MiddleName);
            Assert.AreEqual(publisherFromDb.LastName, result.LastName);
            Assert.AreEqual(publisherFromDb.UserId, result.UserId);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetPubslisherOffersReturnPublisherOffersById()
        {
            var result = this.publisherService.GetPublisherOffers(2);
            var publisherOffers = this.data.Publishers.FirstOrDefault(x => x.Id == 2);

            Assert.AreEqual(publisherOffers.Offers.Count(), result.Count());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void DeletePublisherByIdSouldRemovePublisherFromDb()
        {
            var publishersBefore = this.data.Publishers.Count();
            this.publisherService.DeletePublisherById(2);
            var publishersAfter = this.data.Publishers.Count();

            Assert.AreNotEqual(publishersBefore, publishersAfter);

            this.data.Database.EnsureDeleted();
        }
    }
}
