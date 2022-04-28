namespace InfluencerWannaBeUnitTest.Controllers
{
    using InfluencerWannaBe.Controllers;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBeUnitTest.Mocks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Security.Claims;

    [TestFixture]
    public class InfluencerControllerTest
    {
        [SetUp]
        public void Setup()
        {

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
            InfluencersController influencerController = new InfluencersController(null, null, GetCollectionMock.Instance, null);
            var result = influencerController.AddAccaunt();

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void AddAccauntShouldReturnReddirect()
        {
            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.Email, "email@email.com"),
               new Claim(ClaimTypes.NameIdentifier, "noname"),
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            //Thread.CurrentPrincipal = fakeClaimsPrincipal;
            InfluencersController influencerController = new InfluencersController(DatabaseMock.Instance, InfluencerServiceMock.Instance, GetCollectionMock.Instance, null);
            InfluencerRegistrationFormModel inflregmodel = new InfluencerRegistrationFormModel
            {
                CountryId = 2,
                GenderId = 1,
                Username = "TestTest",
                Email = "testovEmail@test.com",
            };

            var formFile = new Mock<IFormFile>();
            formFile.Setup(x => x.FileName).Returns("test");

            influencerController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };

            var result = influencerController.AddAccaunt(inflregmodel, formFile.Object);

            Assert.NotNull(result);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.RedirectToActionResult", result.GetType().ToString());
        }
    }
}