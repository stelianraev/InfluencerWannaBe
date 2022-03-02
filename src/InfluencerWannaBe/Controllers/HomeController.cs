namespace InfluencerWannaBe.Controllers
{
    using System.Diagnostics;
    using InfluencerWannaBe.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Error() => View();
        
    }
}
