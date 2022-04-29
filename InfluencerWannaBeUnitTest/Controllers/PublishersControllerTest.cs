namespace InfluencerWannaBeUnitTest.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    using InfluencerWannaBe.Controllers;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBeUnitTest.Mocks;
    using InfluencerWannaBe.Models.Publishers;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Influencers;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBe.Services.Publisher;

    using NUnit.Framework;

    [TestFixture]
    public class PublishersControllerTest
    {
        private InfluencerWannaBeDbContext data;
        private IGetCollection getCollection;
        private IOfferService offerService;
        private IPublisherService publisherService;
        private PublishersController publisherController;
        private IInfluencerService influencerService;

        [SetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.getCollection = GetCollectionMock.Instance;
            this.offerService = OfferServiceMock.Instance;
            this.publisherService = PublisherServiceMock.Instance;
            this.influencerService = InfluencerServiceMock.Instance;

            this.publisherController = new PublishersController(this.data, this.publisherService, this.influencerService,  this.getCollection, this.offerService);

            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
               new Claim(ClaimTypes.NameIdentifier, "testovUser")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            this.publisherController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };
        }

        [Test]
        public void BecomePublisherShouldReturnConcretteView()
        {
           var result = this.publisherController.BecomePublisher();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void BecomePublisherShouldReddirectAfterSuccessfulPublisherRegistration()
        {
            PublisherRegistrationFormModel publisher = new PublisherRegistrationFormModel();
            publisher.CountryId = 2;
            publisher.GenderId = 2;
            publisher.Username = "UniqueUsername";
            publisher.Email = "UniqueEmail@uniqueEmail.com";

            var publishersBeforeAdd = this.data.Publishers.Count();

            var result = this.publisherController.BecomePublisher(publisher, null);

            var publishersAfterAdd = this.data.Publishers.Count();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual(publishersBeforeAdd + 1, publishersAfterAdd);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void PublishersShouldRetturnViewWithAllPublishersInDb()
        {
            var query = new AllPublishersQueryModel();
            var result = this.publisherController.Publishers(query);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(3, query.TotalElements);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void DetailsShoildReturnPublisherDetails()
        {
            var result = this.publisherController.Details(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(1, this.data.Publishers.Where(x => x.Id == 2).ToList().Count);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void PublishOfferShouldReturnCorrectView()
        {
           var result = this.publisherController.PublisherOffer();

           Assert.IsNotNull(result);
           Assert.IsInstanceOf<ViewResult>(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void AssignedInfluencersShouldReturnAllAssignedInfluencers()
        {
            var result = this.publisherController.AsignedInfluencers(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(1, this.data.InfleuncerOffers.Where(x => x.Id == 2).ToList().Count);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void AcceptedInfluencersShouldReddirectAffterAcceptanInfluencerForOffer()
        {
            var result = this.publisherController.AcceptInfluencer(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void DeclineInfluencersShouldReddirectAffterPublisherDeclineInfluencer()
        {
            var result = this.publisherController.DeclineInfluencer(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void DeletePublisherShouldRemovePublisherFromDBAndReddirect()
        {
            var realPublisherService = new PublisherService(this.data);
            PublishersController controller = new(this.data, realPublisherService, this.influencerService, this.getCollection, this.offerService);

            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
               new Claim(ClaimTypes.NameIdentifier, "testovUser")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };

            var pulisherCountBefore = this.data.Publishers.Count();

            var result = controller.Delete(2);

            var pulisherCountAfter = this.data.Publishers.Count();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual(pulisherCountBefore - 1, pulisherCountAfter);

            this.data.Database.EnsureDeleted();
        }
    }
}
