namespace InfluencerWannaBe.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Influencers;
    using InfluencerWannaBe.Models;
    using InfluencerWannaBe.Services.Publisher;
    using InfluencerWannaBe.Infrastructure;

    public class HomeController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;
        private readonly IInfluencerService influencers;
        private readonly IPublisherService publishers;
        private IEmailSender emailSender;

        public HomeController(InfluencerWannaBeDbContext data, IInfluencerService influencers, IPublisherService publishers, IEmailSender emailSender)        
        {
            this.data = data;
            this.influencers = influencers;
            this.publishers = publishers;
            this.emailSender = emailSender;
        }
        
        public IActionResult Index() => View();

        public IActionResult Error() => View();

        [Authorize]
        public IActionResult EmailSending(int id)
        {
            return this.View(new EmailFormModel()
            {
                RecepientEmail = publishers.GetPublisher(id).Email,
                SenderEmail = User.GetEmail()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult EmailSending(EmailFormModel email)
        {
            if (!ModelState.IsValid)
            {
                return View(email);
            }
            this.emailSender.SendEmail(email.RecepientEmail, email.SenderEmail, email.Body);
            return View("SuccessEmailSent");
        }
    }
}
