namespace InfluencerWannaBe.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authorization;

    public class InfluencersController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;

        public InfluencersController(InfluencerWannaBeDbContext data) => this.data = data;

        [Authorize]
        public IActionResult AddAccaunt() => View(new InfluencerRegistrationFormModel
        {
            Conutries = this.GetInfluencerCountries(),
            Genders = this.GetInfluencerGender(),
        });

        [Authorize]
        [HttpPost]
        public IActionResult AddAccaunt(InfluencerRegistrationFormModel influencer, IFormFile photo)
        {
            if (photo == null || photo.Length > 5 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Photo", "Image is too big. Max size is 5MB");
            }

            if (!this.data.Countries.Any(x => x.Id == influencer.CountryId))
            {
                this.ModelState.AddModelError(nameof(influencer.CountryId), "Country do not exist");
            }

            if (!this.data.Genders.Any(x => x.Id == influencer.GenderId))
            {
                this.ModelState.AddModelError(nameof(influencer.GenderId), "Gender do not exist");
            }

            if (!ModelState.IsValid)
            {
                influencer.Conutries = this.GetInfluencerCountries();
                influencer.Genders = this.GetInfluencerGender();

                return View(influencer);
            }

            var imageInMemory = new MemoryStream();
            photo.CopyTo(imageInMemory);
            var imageBytes = imageInMemory.ToArray();

            var influencerData = new Influencer
            {
                FirstName = influencer.FirstName,
                MiddleName = influencer.MiddleName,
                LastName = influencer.LastName,
                Age = influencer.Age,
                GenderId = influencer.GenderId,
                Username = influencer.Username,
                CountryId = influencer.CountryId,
                Description = influencer.Description,
                Email = influencer.Email,
                PhoneNumber = influencer.PhoneNumber,
                InstagramUrl = influencer.InstagramUrl,
                FacebookUrl = influencer.FacebookUrl,
                TwitterUrl = influencer.TwitterUrl,
                YouTubeUrl = influencer.YouTubeUrl,
                TikTokUrl = influencer.TikTokUrl,
                Photo = imageBytes,
                WebSiteUrl = influencer.WebSiteUrl
            };

            this.data.Influencers.Add(influencerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(Influencers));
        }

        public IActionResult Influencers([FromQuery] AllInfluencersQueryModel query) 
        {
            var influencersQuery = this.data.Influencers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                influencersQuery = influencersQuery.Where(i =>
                   (i.FirstName + " " + i.LastName).ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.FirstName.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.Username.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.LastName.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            influencersQuery = query.Sorting switch
            { 
               InfluencerSorting.Username => influencersQuery.OrderBy(i => i.Username),
               InfluencerSorting.FirstName => influencersQuery.OrderBy(i => i.FirstName),
               InfluencerSorting.Age => influencersQuery.OrderBy(i => i.Age),
               InfluencerSorting.DateCreated or _=> influencersQuery.OrderByDescending(i => i.Id),
            };

            var totalInfluencers = influencersQuery.Count();

            var influencers = influencersQuery
                .Skip((query.CurrentPage - 1) * AllInfluencersQueryModel.InfluencersPerPage)
                .Take(AllInfluencersQueryModel.InfluencersPerPage)
                .Select(i => new InfluencerListingViewModel
                {
                    Id = i.Id,
                    Username = i.Username,
                    Instagram = i.InstagramUrl,
                    Facebook = i.FacebookUrl,
                    Photo = i.Photo
                })
                .ToList();

            query.TotalInfluencers = totalInfluencers;
            query.Influencers = influencers;

            return View(query);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var selected = this.data.Influencers
                .Where(x => x.Id == id)
                .Select(x => new InfluencerViewModel
                {
                    Photo = x.Photo,
                    Username = x.Username,
                    FacebookUrl = x.FacebookUrl,
                    InstagramUrl = x.InstagramUrl,
                    TwitterUrl = x.TwitterUrl,
                    CountryName = x.Country.Name,
                    Email = x.Email
                })
                .FirstOrDefault();
                
            return this.View(selected);
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
