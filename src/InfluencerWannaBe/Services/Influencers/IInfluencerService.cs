using InfluencerWannaBe.Data.Models;

namespace InfluencerWannaBe.Services.Influencers
{
    public interface IInfluencerService
    {
        bool IsInfluencer(string userId);

        Influencer GetInfluencer(string id);

        int IdByUser(string userId);
    }
}
