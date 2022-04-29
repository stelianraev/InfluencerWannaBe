namespace InfluencerWannaBeUnitTest.Controllers
{
    using System.Security.Claims;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    using InfluencerWannaBe.Controllers;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Influencers;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBeUnitTest.Mocks;

    using Moq;
    using NUnit.Framework;
    using MyTested.AspNetCore.Mvc;

    [TestFixture]
    public class InfluencerControllerTest
    {
        private InfluencerWannaBeDbContext data;
        private IInfluencerService influencerService;
        private IGetCollection getCollection;
        private IOfferService offerService;
        private InfluencersController influencerController;

        [SetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.influencerService = InfluencerServiceMock.Instance;
            this.getCollection = GetCollectionMock.Instance;
            this.offerService = OfferServiceMock.Instance;

            this.influencerController = new InfluencersController(this.data, this.influencerService, this.getCollection, this.offerService);
                       
        }

       //[Test]
       //public void AddAccauntShouldReturnCorrectViewWithModel()
       // => MyController<InfluencersController>
       //   .Instance(controller => controller
       //   .WithDependencies(DatabaseMock.Instance, null, GetCollectionMock.Instance, null))
       //   .Calling(c => c.AddAccaunt())
       //   .ShouldReturn()
       //   .View(view => view
       //   .WithModelOfType<InfluencerRegistrationFormModel>()
       //   .Passing(model =>
       //   {
       //       Assert.AreEqual(6, model.Conutries.Count);
       //       Assert.AreEqual(2, model.Genders.Count);
       //   }));

        [Test]
        public void AddAccauntShouldReturnCorrectViewWithModel()
        {
            var result = this.influencerController.AddAccaunt();

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            this.data.Database.EnsureDeleted();

        }

        [Test]
        public void AddAccauntShouldReturnReddirect()
        {
            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
               new Claim(ClaimTypes.NameIdentifier, "noname")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            InfluencerRegistrationFormModel inflregmodel = new InfluencerRegistrationFormModel
            {
                CountryId = 2,
                GenderId = 1,
                Username = "TestTest",
                Email = "testovEmail@test.com"
            };

            var formFile = new Mock<IFormFile>();
            formFile.Setup(x => x.FileName).Returns("test");

            this.influencerController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };

            var result = this.influencerController.AddAccaunt(inflregmodel, formFile.Object);

            Assert.NotNull(result);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.RedirectToActionResult", result.GetType().ToString());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void InfluencersShouldReturnViewWithInfluencers()
        {
            MyController<InfluencersController>
             .Instance(controller => controller
             .WithDependencies(this.data, this.influencerService, this.getCollection, null))
             .Calling(c => c.Influencers(new AllInfluencersQueryModel()))
             .ShouldReturn()
             .View(view => view
             .WithModelOfType<AllInfluencersQueryModel>()
             .Passing(x => x.TotalElements == 6));

              this.data.Database.EnsureDeleted();
        }

        [Test]
        public void InfluencersAttributeValidate()
        {
            MyController<InfluencersController>
              .Calling(c => c.Influencers(new AllInfluencersQueryModel()))
              .ShouldHave()
              .ActionAttributes(attributes => attributes
              .RestrictingForAuthorizedRequests());

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void DetailsShouldReturnViewWithInfluencer()
        {
            InfluencersController influencerController = new InfluencersController(this.data, InfluencerServiceMock.Instance, GetCollectionMock.Instance, null);
            var result = influencerController.Details(2);

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            this.data.Database.EnsureDeleted();
        }

        [Test]
        public void SignInOffersShouldReturnViewWithOffers()
        {
            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.NameIdentifier, "email@email.com"),
                new Claim(ClaimTypes.NameIdentifier, "noname")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;

            InfluencersController influencerController = new InfluencersController(this.data, this.influencerService, this.getCollection, this.offerService);

            influencerController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };

            var result = influencerController.SignInOffers();

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);

            this.data.Database.EnsureDeleted();
        }

        // => MyController<InfluencersController>
        //   .Instance(controller => controller
        //   .WithDependencies(DatabaseMock.Instance, InfluencerServiceMock.Instance, GetCollectionMock.Instance, null))
        //   //.WithData(DatabaseMock.Instance))
        //   .Calling(c => c.Details(3))
        //   .ShouldReturn()
        //   .View(view => view
        //   .WithModelOfType<InfluencerViewModel>()
        //   .Passing(x => 
        //            x.Id == 2 &&
        //            x.Age == 21 &&
        //            x.CountryId == 5 &&
        //            x.Email == "influencer2@gmail.com" &&
        //            x.Username == "infl2" &&
        //            x.FirstName == "test2" &&
        //            x.LastName == "test2lastname"));

    }
}