namespace InfluencerWannaBe.Models.Offers
{
    using System.Collections.Generic;

    public class AllOffersQueryModel
    {
        public const int OffersPerPage = 2;
        public int PublisherId { get; set; }
        public string SearchTerm { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int TotalOffers { get; set; }
        public OffersSorting Sorting { get; init; }
        public IEnumerable<OffersListingViewModel> Offers { get; set; }
    }
}
