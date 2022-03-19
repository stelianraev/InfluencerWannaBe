namespace InfluencerWannaBe.Services.Influencers
{
    public interface IInfluencerService
    {
        bool IsInfluencer(string userId);

        int IdByUser(string userId);
    }
}
