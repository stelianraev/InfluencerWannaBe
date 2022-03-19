namespace InfluencerWannaBe.Models.Influencers
{
    using System.Collections.Generic;

    public class AllInfluencersQueryModel 
    {
        //How many influencers per page we want
        public const int InfluencersPerPage = 2;

        public string SearchTerm { get; init; }
        public int CurrentPage { get; init; } = 1;
        public int TotalInfluencers { get; set; }
        public InfluencerSorting Sorting { get; init; }
        public IEnumerable<InfluencerListingViewModel> Influencers { get; set; }
    }
}
