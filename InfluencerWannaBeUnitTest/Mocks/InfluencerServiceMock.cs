using InfluencerWannaBe.Data.Models;
using InfluencerWannaBe.Services.Influencers;
using Moq;

namespace InfluencerWannaBeUnitTest.Mocks
{
    public static class InfluencerServiceMock
    {
        public static IInfluencerService Instance
        {
            get
            {
                var mock = new Mock<IInfluencerService>();
                mock.Setup(x => x.GetInfluencer(It.IsAny<string>())).Returns(new Influencer
                {
                    Username = "Username",
                    CountryId = 2,
                    Email = "test@test.com",
                    FirstName = "FirstName",
                    MiddleName = "MiddleName",
                    LastName = "LastName",
                    Id = 5,
                    PhoneNumber = "phoneNumber"
                });

                mock.Setup(x => x.IdByUser(It.IsAny<string>())).Returns(0);

                return mock.Object;
            }
        }
    }
}
