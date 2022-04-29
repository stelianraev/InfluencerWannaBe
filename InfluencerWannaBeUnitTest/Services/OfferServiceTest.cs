namespace InfluencerWannaBeUnitTest.Services
{
    using System.Linq;

    using NUnit.Framework;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBeUnitTest.Mocks;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBe.Services.Influencers;

    [TestFixture]
    public class OfferServiceTest
    {
        private InfluencerWannaBeDbContext data;
        private IOfferService offerService;
        private IInfluencerService influencerService;

        [SetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.influencerService = new InfluencerService(this.data);

            this.offerService = new OfferService(this.data, this.influencerService);

        }

        [Test]
        public void AddOfferToInfluencerAddingOfferToInfluencerInDb()
        {
            this.offerService.AddOfferToInfluencer(3, new InfluencerOffers());
            var result = this.data.Influencers.FirstOrDefault(x => x.Id == 3);

            Assert.AreEqual(2, result.SignUpOffers.Count());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void OffersByUserIdShouldReturnCollectionFromOffers()
        {
            var result = this.offerService.OffersByUser("testovUser");

            Assert.AreEqual(1, result.Count());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void OffersBySignInInfluencerShouldReturnCollectionFromModel()
        {
            var result = this.offerService.OffersBySignInInfluencer("testovUser");

            Assert.AreEqual(1, result.Count());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void DeleteOfferbyIdShouldRemoveOfferFromDBWithSpecificId()
        {
            var offersBefore = this.data.Offers.Count();

            this.offerService.DeleteOfferById(2);

            var offersAfter = this.data.Offers.Count();

            Assert.AreEqual(offersBefore - 1, offersAfter);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetOfferShouldReturnSpecificOfferById()
        {
            var result = this.offerService.GetOffer(2);
            var selectedOffer = this.data.Offers.FirstOrDefault(x => x.Id == 2);

            Assert.AreEqual(selectedOffer.Id, result.Id);
            Assert.AreEqual(selectedOffer.CountryId, result.CountryId);
            Assert.AreEqual(selectedOffer.CreationDate, result.CreationDate);
            Assert.AreEqual(selectedOffer.Description, result.Description);
            Assert.AreEqual(selectedOffer.ExpireDate, result.ExpireDate);
            Assert.AreEqual(selectedOffer.IsExpired, result.IsExpired);
            Assert.AreEqual(selectedOffer.IsPossibleToSignIn, result.IsPossibleToSignIn);
            Assert.AreEqual(selectedOffer.OwnerId, result.OwnerId);
            Assert.AreEqual(selectedOffer.Payment, result.Payment);
            Assert.AreEqual(selectedOffer.PublisherId, result.PublisherId);
            Assert.AreEqual(selectedOffer.Requirents, result.Requirents);
            Assert.AreEqual(selectedOffer.Title, result.Title);

            this.data.Database.EnsureDeleted();
        }
    }
}
