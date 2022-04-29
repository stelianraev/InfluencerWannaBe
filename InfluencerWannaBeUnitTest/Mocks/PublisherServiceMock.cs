namespace InfluencerWannaBeUnitTest.Mocks
{
    using Moq;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Services.Publisher;

    public static class PublisherServiceMock
    {
        public static IPublisherService Instance
        {
            get
            {
                var publisher = new Publisher
                {
                    Username = "Username",
                    CountryId = 2,
                    Email = "test@test.com",
                    FirstName = "FirstName",
                    MiddleName = "MiddleName",
                    LastName = "LastName",
                    Id = 5,
                    PhoneNumber = "phoneNumber"
                };

                var mock = new Mock<IPublisherService>();
                mock.Setup(x => x.GetPublisher(It.IsAny<int>())).Returns(publisher);

                mock.Setup(x => x.IsPublisher(It.IsAny<string>())).Returns(true);

                return mock.Object;
            }
        }
    }
}
