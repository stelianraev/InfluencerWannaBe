using InfluencerWannaBe.Data;
using InfluencerWannaBe.Data.Models;
using InfluencerWannaBe.Infrastructure;
using InfluencerWannaBe.Models.Publishers;
using InfluencerWannaBe.Services;
using InfluencerWannaBe.Services.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace InfluencerWannaBe.Controllers
{
    public class PublishersContoller : Controller
    {
        private readonly IPublisherService publishers;
        private readonly InfluencerWannaBeDbContext data;
        private readonly IGetCollection getCollection;

        public PublishersContoller(InfluencerWannaBeDbContext data,IPublisherService publishers, IGetCollection getCollection)
        {
            this.data = data;
            this.publishers = publishers;
            this.getCollection = getCollection;
        }

        [Authorize]
        public IActionResult BecomePublisher() => View(new PublisherRegistrationFormModel
        {
            Conutries = this.getCollection.GetCountries(),
            Genders = this.getCollection.GetGender()
        });

        [Authorize]
        [HttpPost]
        public IActionResult BecomePublisher(PublisherRegistrationFormModel publisher, IFormFile photo)
        {
            var publisherId = this.publishers.IdByUser(this.User.GetId());

            if (photo == null || photo.Length > 5 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Photo", "Image is too big. Max size is 5MB");
            }

            if (!this.data.Countries.Any(x => x.Id == publisher.CountryId))
            {
                this.ModelState.AddModelError(nameof(publisher.CountryId), "Country do not exist");
            }

            if (!this.data.Genders.Any(x => x.Id == publisher.GenderId))
            {
                this.ModelState.AddModelError(nameof(publisher.GenderId), "Gender do not exist");
            }
            if (publisherId != 0)
            {
                this.ModelState.AddModelError(nameof(publisher), "Influencer already exist");
            }

            if (!ModelState.IsValid)
            {
                publisher.Conutries = this.getCollection.GetCountries();
                publisher.Genders = this.getCollection.GetGender();

                return View(publisher);
            }

            var imageInMemory = new MemoryStream();
            photo.CopyTo(imageInMemory);
            var imageBytes = imageInMemory.ToArray();

            var publisherData = new Publisher
            {
                FirstName = publisher.FirstName,
                MiddleName = publisher.MiddleName,
                LastName = publisher.LastName,
                GenderId = publisher.GenderId,
                Username = publisher.Username,
                CountryId = publisher.CountryId,
                Description = publisher.Description,
                Email = User.GetEmail(),
                PhoneNumber = publisher.PhoneNumber,
                InstagramUrl = publisher.InstagramUrl,
                FacebookUrl = publisher.FacebookUrl,
                TwitterUrl = publisher.TwitterUrl,
                YouTubeUrl = publisher.YouTubeUrl,
                TikTokUrl = publisher.TikTokUrl,
                Photo = imageBytes,
                WebsiteUrl = publisher.WebsiteUrl,
                UserId = User.GetId()
            };

            this.data.Publishers.Add(publisherData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(publisherData));
        }
    }
}
