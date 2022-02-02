﻿namespace InfluencerWannaBe.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.IO;

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
        public IActionResult Register(InfluencerRegistrationFormModel influencer, IFormFile photo)
        {
            if(photo == null || photo.Length > 5 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Photo","Image is too big. Max size is 5MB");
            }

            if(!this.data.Countries.Any(x => x.Id == influencer.CountryId))
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

            return RedirectToAction("Index", "Home");
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
