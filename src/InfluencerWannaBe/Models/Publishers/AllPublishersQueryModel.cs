namespace InfluencerWannaBe.Models.Publishers
{
    using System.Collections.Generic;

    public class AllPublishersQueryModel
    {
        //How many influencers per page we want
        public const int PublishersPerPage = 3;

        public string SearchTerm { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int TotalPublishers { get; set; }
        public PublisherSorting Sorting { get; init; }
        public IEnumerable<PublisherListingViewModel> Publishers { get; set; }
    }
}
