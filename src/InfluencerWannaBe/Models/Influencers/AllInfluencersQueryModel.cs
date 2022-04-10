namespace InfluencerWannaBe.Models.Influencers
{
    public class AllInfluencersQueryModel : PageSettingsAbstract<InfluencerSorting, InfluencerListingViewModel>
    {
        //How many influencers per page we want
        public const int InfluencersPerPage = 3;
    }
}
