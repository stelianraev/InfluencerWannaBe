namespace InfluencerWannaBeUnitTest.Controllers
{
    using InfluencerWannaBe.Controllers;
    using InfluencerWannaBe.Models;
    using InfluencerWannaBeUnitTest.Mocks;
    using Microsoft.AspNetCore.Http;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Security.Claims;

    [TestFixture]
    public class HomeControllerTest
    {
        [SetUp]
        public void Setup()
        {
            
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

            HomeController homeController = new HomeController(DatabaseMock.Instance, null, PublisherServiceMock.Instance, null);

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
           .WithDependencies(DatabaseMock.Instance, null, PublisherServiceMock.Instance, EmailSenderMock.Instance))
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