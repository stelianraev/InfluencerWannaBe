namespace InfluencerWannaBeUnitTest.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Controllers;
    using InfluencerWannaBe.Models.Offers;
    using InfluencerWannaBeUnitTest.Mocks;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Influencers;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBe.Services.Publisher;

    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    [TestFixture]
    public class OffersControllerTest
    {
        private InfluencerWannaBeDbContext data;
        private IGetCollection getCollection;
        private IOfferService offerService;
        private IPublisherService publisherService;
        private OffersController offersController;
        private IInfluencerService influencerService;

        [SetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.getCollection = GetCollectionMock.Instance;
            this.offerService = OfferServiceMock.Instance;
            this.publisherService = PublisherServiceMock.Instance;
            this.influencerService = InfluencerServiceMock.Instance;

            this.offersController = new OffersController(this.data, this.publisherService, this.getCollection, this.offerService);

            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
               new Claim(ClaimTypes.NameIdentifier, "testovUser")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            this.offersController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };
        }

        [Test]
        public void OffersShouldReturnCorrectViewWithICollectionWithOfOffers()
        {
           var result = this.offersController.Offers(new AllOffersQueryModel(), 2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void DeleteShouldRemoveOfferFromDBAndReddirectToOffersView()
        {
            OfferService offerServ = new OfferService(this.data, this.influencerService);
            OffersController offersControl = new OffersController(this.data, this.publisherService, this.getCollection, offerServ);

            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            offersControl.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };

           
            var result = offersControl.Delete(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual(1, this.data.Offers.Count());
            
            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void EditShouldReturnCorrectViewWithICollectionWithEditedOffer()
        {
            OffersRegistrationFormModel regModel = new OffersRegistrationFormModel();
            regModel.Title = "EditedModel";
            regModel.Description = "No Description";
            regModel.Requirements = "Tested Requerements";
            regModel.Payment = 23.3;
            regModel.CountryId = 2;
            regModel.IsPossibleToSignIn = true;

            OfferService offerServ = new OfferService(this.data, this.influencerService);
            OffersController offersControl = new OffersController(this.data, this.publisherService, this.getCollection, offerServ);

            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
               new Claim(ClaimTypes.NameIdentifier, "testovUser")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            offersControl.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };


            var result = offersControl.Edit(regModel, null, 2);

            var updatedOffer = this.data.Offers.FirstOrDefault(x => x.Id == 2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual(regModel.Title, updatedOffer.Title);
            Assert.AreEqual(regModel.Description, updatedOffer.Description);
            Assert.AreEqual(regModel.Requirements, updatedOffer.Requirents);
            Assert.AreEqual("testovUser", updatedOffer.OwnerId);
            Assert.AreEqual(regModel.Payment, updatedOffer.Payment);
            Assert.AreEqual(regModel.IsPossibleToSignIn, updatedOffer.IsPossibleToSignIn);

            this.data.Database.EnsureDeleted();
        }
        [Test]
        public void EditShouldReturnViewWithSelectedOffer()
        {
            MyController<OffersController>
                .Instance(controller => controller
                .WithDependencies(this.data, this.publisherService, this.getCollection, this.offerService))
                .Calling(c => c.Edit(2))
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<OffersRegistrationFormModel>()
                .Passing(x => x.CountryId == 2 &&
                x.Title == "title" &&
                x.CountryId == 2 &&
                x.Title == "title" &&
                x.Description == "DescriptionWithManySymbols" &&
                x.Payment == 22));

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void RemoveShouldRemoveOfferByIdAndReddirect()
        {
            OfferService offerServ = new OfferService(this.data, this.influencerService);
            OffersController offersControl = new OffersController(this.data, this.publisherService, this.getCollection, offerServ);

            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
               new Claim(ClaimTypes.NameIdentifier, "testovUser")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            offersControl.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };


            var result = offersControl.Remove(2);

            Assert.AreEqual(2, this.data.InfleuncerOffers.Count());
            Assert.IsNotNull(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void AddOfferShouldReturnCorrectView()
        {
           var result = this.offersController.AddOffer();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void AddOfferShouldAddOfferToPublisherAndReddirectToOffersOfTheSamePublisher()
        {
            OffersRegistrationFormModel offer = new OffersRegistrationFormModel();
            offer.CountryId = 2;
            offer.Title = "Test";
            offer.Description = "Description with minimum symbols";
            offer.Requirements = "meet the requirements";
            offer.Payment = 22.50;
            offer.IsPossibleToSignIn = true;

            var offersBeforeAdd = this.data.Offers.Count();

            var result = this.offersController.AddOffer(offer, null);

            var offersAfterAdd = this.data.Offers.Count();

            Assert.AreEqual(offersBeforeAdd + 1, offersAfterAdd);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            this.data.Database.EnsureDeleted();

        }

        [Test]
        public void DetailsShouldReturnCorrectViewWithOfferDetailModel()
        {
            MyController<OffersController>
              .Instance(constroller => constroller
              .WithDependencies(this.data, this.publisherService, this.getCollection, this.offerService))
              .Calling(c => c.Details(2))
              .ShouldReturn()
              .View(view => view
              .WithModelOfType<OfferViewModel>()
              .Passing(x => x.Id == 2 &&
                            x.Title == "testing3"));

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void SignUpShouldAssignInfluencerToOffer()
        {
            var inflOffbefore = this.data.InfleuncerOffers.Count();

            var result = this.offersController.SignUp(2);

            var inflOffafter = this.data.InfleuncerOffers.Count();

            Assert.AreEqual(inflOffbefore + 1, inflOffafter);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            this.data.Database.EnsureDeleted();
        }
    }
}
