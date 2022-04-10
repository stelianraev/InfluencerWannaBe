namespace CarRentingSystem.Areas.Admin.Controllers
{
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Influencers;
    using InfluencerWannaBe.Services.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using static InfluencerWannaBe.Models.Constants.AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;
        private readonly IInfluencerService influencers;
        private readonly IGetCollection getCollection;
        private readonly IOfferService offerService;

        public AdminController(InfluencerWannaBeDbContext data, IInfluencerService influencers, IGetCollection getCollection, IOfferService offerService)
        {
            this.influencers = influencers;
            this.data = data;
            this.getCollection = getCollection;
            this.offerService = offerService;
        }

        [Authorize]
        public IActionResult RemoveInfluencer(int id)
        {
            var influencer = this.data.Influencers.FirstOrDefault(x => x.Id == id);

            this.data.Influencers.Remove(influencer);
            this.data.SaveChanges();

            return RedirectToAction("SignInOffers", "Influencers"); //nameof(InfluencersController.SignInOffers));            
        }
    }
}
