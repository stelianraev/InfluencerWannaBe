namespace InfluencerWannaBeUnitTest.Mocks
{
    using System;
    using System.Collections.Generic;

    using Moq;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Models.Offers;
    using InfluencerWannaBe.Services.Offers;

    public static class OfferServiceMock
    {
        public static IOfferService Instance
        {
            get
            {
                var mock = new Mock<IOfferService>();
                mock.Setup(x => x.OffersByUser(It.IsAny<string>())).Returns(new List<OffersListingViewModel>()
                                                                                    {new OffersListingViewModel
                                                                                                        { Id = 2,
                                                                                                          OfferId = 2,
                                                                                                          CreationDate = DateTime.Now
                                                                                                        }});

                mock.Setup(x => x.GetOffer(It.IsAny<int>())).Returns(new Offer
                {
                    Title = "title",
                    CountryId = 2,
                    OwnerId = "owner",
                    Description = "DescriptionWithManySymbols",
                    Payment = 22
                });

                return mock.Object;
            }
        }
    }
}
