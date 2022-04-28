namespace InfluencerWannaBe.Areas.Admin.Controllers
{
    using InfluencerWannaBe.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using static InfluencerWannaBe.Areas.Admin.AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;

        public AdminController(InfluencerWannaBeDbContext data)
        {
            this.data = data;
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
