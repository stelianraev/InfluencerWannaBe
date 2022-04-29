namespace InfluencerWannaBeUnitTest.Mocks
{
    using Moq;
    using InfluencerWannaBe.Services;

    public static class EmailSenderMock
    {
        public static IEmailSender Instance
        {
            get
            {
                var mock = new Mock<IEmailSender>();
                mock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

                return mock.Object;
            }
        }
    }
}
