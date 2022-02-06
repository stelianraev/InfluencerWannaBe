namespace InfluencerWannaBe.Models.Influencers
{
    using System.Collections.Generic;

    public class AllInfluencersQueryModel
    {
        public string SearchTerm { get; init; }
        public InfluencerSorting Sorting { get; init; }
        public IEnumerable<InfluencerListingViewModel> Influencers { get; set; }
    }
}
