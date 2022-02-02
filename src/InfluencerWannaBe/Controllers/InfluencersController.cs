namespace InfluencerWannaBe.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBe.Data;

    public class InfluencersController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;

        public InfluencersController(InfluencerWannaBeDbContext data) => this.data = data;
        public IActionResult Register() => View(new InfluencerRegistrationFormModel
        {
            Conutries = this.GetInfluencerCountries(),
            Genders = this.GetInfluencerGender(),
        });

        [HttpPost]
        public IActionResult Register(InfluencerRegistrationFormModel influencer)
        {

            return View();
        }

        private IEnumerable<InfluencerCountryViewModel> GetInfluencerCountries()
        => this.data
            .Countries
            .Select(c => new InfluencerCountryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    
        private IEnumerable<InfluencerGenderViewModel> GetInfluencerGender()
        => this.data
           .Genders
           .Select(c => new InfluencerGenderViewModel
           {
               Id = c.Id,
               Name = c.Name
           })
           .ToList();
    }
}
