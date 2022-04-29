namespace InfluencerWannaBeUnitTest.Controllers
{
    using System.Security.Claims;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Models;
    using InfluencerWannaBe.Controllers;
    using InfluencerWannaBeUnitTest.Mocks;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Influencers;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBe.Services.Publisher;

    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;

    [TestFixture]
    public class HomeControllerTest
    {
        private InfluencerWannaBeDbContext data;
        private IInfluencerService influencerService;
        private IGetCollection getCollection;
        private IOfferService offerService;
        private IPublisherService publisherService;

        [OneTimeSetUp]
        public void Setup()
        {
            this.data = DatabaseMock.Instance;
            this.influencerService = InfluencerServiceMock.Instance;
            this.getCollection = GetCollectionMock.Instance;
            this.offerService = OfferServiceMock.Instance;
            this.publisherService = PublisherServiceMock.Instance;

           this.data.Database.EnsureDeleted();
        }

        [Test]
        public void IndexShouldReturnView()
        => MyController<HomeController>
            .Instance(controller => controller
            .WithoutData())
            .Calling(c => c.Index())
            .ShouldReturn()
            .View(view => view
            .WithDefaultName());

        [Test]
        public void ErrorShouldReturnView()
        => MyController<HomeController>
            .Instance(controller => controller
            .WithoutData())
            .Calling(c => c.Error())
            .ShouldReturn()
            .View(view => view
            .WithDefaultName());

        [Test]
        public void EmailSendingShouldReturnCorrectViewWithModel()
        {
            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;

            HomeController homeController = new HomeController(this.data, null, this.publisherService, null);

            homeController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };

            homeController.EmailSending(5);
        }

        [Test]
        public void EmailSendingAttributesTest()
        => MyController<HomeController>
            .Calling(c => c.EmailSending(new EmailFormModel
            { SenderEmail = "sendetest@sender.com",
                RecepientEmail = "recepient@recepient.com",
                Body = "bodytest" }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests()
            .RestrictingForHttpMethod(HttpMethod.Post));


       [Test]
       public void EmailSendingShouldReturnCorrectViewPost()
       => MyController<HomeController>
           .Instance(controller => controller
           .WithDependencies(this.data, null, this.publisherService, EmailSenderMock.Instance))
           .Calling(c => c.EmailSending(new EmailFormModel
           {
               SenderEmail = "sender@sender.com",
               RecepientEmail = "recepiente@recepient.com",
               Body = "bodytestmustbetensymbols"
           }))
           .ShouldReturn()
           .View(view => view
           .WithNoModel());            
    }
}