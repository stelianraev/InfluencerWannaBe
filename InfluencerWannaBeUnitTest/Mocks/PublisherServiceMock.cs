using InfluencerWannaBe.Data.Models;
using InfluencerWannaBe.Services.Publisher;
using Moq;

namespace InfluencerWannaBeUnitTest.Mocks
{
    public static class PublisherServiceMock
    {
        public static IPublisherService Instance
        {
            get
            {
                var mock = new Mock<IPublisherService>();
                mock.Setup(x => x.GetPublisher(It.IsAny<int>())).Returns(new Publisher
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

                return mock.Object;
            }
        }
    }
}
