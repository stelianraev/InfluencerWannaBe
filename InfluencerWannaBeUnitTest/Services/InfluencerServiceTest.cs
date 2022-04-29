namespace InfluencerWannaBeUnitTest.Services
{
    using System.Linq;

    using NUnit.Framework;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBeUnitTest.Mocks;
    using InfluencerWannaBe.Services.Influencers;

    [TestFixture]
    public class InfluencerServiceTest
    {
        private InfluencerWannaBeDbContext data;
        private IInfluencerService influencerService;

        [SetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.influencerService = new InfluencerService(this.data);            
        }

        [Test]
        public void IsInfluencerCheckIsUserIdIsEquealToSomeInfluencer()
        {
            var result = this.influencerService.IsInfluencer("testovUser");

            Assert.AreEqual(true, result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void IdByUserReturnIdFromUserId()
        {
            var result = this.influencerService.IdByUser("testovUser");

            Assert.AreEqual(2, result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetInfluencerReturnInfluencerFromDbById()
        {
            var result = this.influencerService.GetInfluencer("testovUser");

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual(21, result.Age);
            Assert.AreEqual(5, result.CountryId);
            Assert.AreEqual("influencer2@gmail.com", result.Email);
            Assert.AreEqual("testovUser", result.UserId);
            Assert.AreEqual("infl2", result.Username);
            Assert.AreEqual("test2", result.FirstName);
            Assert.AreEqual("test2lastname", result.LastName);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetInfluencerOffersReturnFirstInfleuncerOffersWhereInfleuncerIsSignUp()
        {
            var influencer = this.influencerService.GetInfluencer("testovUser");
            var result = this.influencerService.GetInfluencerOffer(influencer);
            var influencerOffers = this.data.InfleuncerOffers.FirstOrDefault(x => x.Influencer == influencer);

            Assert.AreEqual(influencerOffers.Id, result.Id);
            Assert.AreEqual(influencerOffers.OfferId, result.OfferId);
            Assert.AreEqual(influencerOffers.InfluencerId, result.InfluencerId);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void GetInfluencerOffersReturnCollectionInfleuncerOffersWhereInfleuncerIsSignUp()
        {
            var influencer = this.influencerService.GetInfluencer("testovUser");
            var inflOffer = this.data.InfleuncerOffers.FirstOrDefault(x => x.Influencer == influencer);
            var result = this.influencerService.InfluencerOffers(inflOffer);
            var influencerOffers = this.data.InfleuncerOffers.Where(x => x.Influencer == influencer).ToList().Count();

            Assert.AreEqual(influencerOffers, result.Count());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void InfluencerOfferInflIdOfferIdReturnSpecificOfferFromInfluencerAndOffer()
        {
            var result = this.influencerService.InfluencerOfferInflIdOfferId(2, 2);

            Assert.AreEqual(2, result.InfluencerId);
            Assert.AreEqual(2, result.OfferId);

            this.data.Database.EnsureDeleted();

        }
    }
}
