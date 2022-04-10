namespace InfluencerWannaBe.Models.Publishers
{
    public class AllPublishersQueryModel : PageSettingsAbstract<PublisherSorting, PublisherListingViewModel>
    {
        //How many influencers per page we want
        public const int PublishersPerPage = 3;   
    }
}
