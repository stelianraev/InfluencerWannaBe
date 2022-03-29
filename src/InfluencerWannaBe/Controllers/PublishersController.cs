using InfluencerWannaBe.Data;
using InfluencerWannaBe.Data.Models;
using InfluencerWannaBe.Infrastructure;
using InfluencerWannaBe.Models.Publishers;
using InfluencerWannaBe.Services;
using InfluencerWannaBe.Services.Influencers;
using InfluencerWannaBe.Services.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace InfluencerWannaBe.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService publishers;
        private readonly IInfluencerService influencers;
        private readonly InfluencerWannaBeDbContext data;
        private readonly IGetCollection getCollection;

        public PublishersController(InfluencerWannaBeDbContext data, IPublisherService publishers, IInfluencerService influencerService, IGetCollection getCollection)
        {
            this.data = data;
            this.publishers = publishers;
            this.getCollection = getCollection;
            this.influencers = influencerService;
        }

        [Authorize]
        public IActionResult BecomePublisher()
        {
            return View(new PublisherRegistrationFormModel
            {
                Conutries = this.getCollection.GetCountries(),
                Genders = this.getCollection.GetGender()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult BecomePublisher(PublisherRegistrationFormModel publisher, IFormFile photo)
        {
            var influencerId = this.influencers.IdByUser(this.User.GetId());
            publisher.Email = User.GetEmail();
            //if (influencerId != 0)
            //{
            //    var influencer = this.data.Influencers.Where(x => x.Id == influencerId).FirstOrDefault();

            //    var pub = new Publisher
            //    {
            //        FirstName = influencer.FirstName,
            //        MiddleName = influencer.MiddleName,
            //        LastName = influencer.LastName,
            //        GenderId = influencer.GenderId,
            //        Username = influencer.Username,
            //        CountryId = influencer.CountryId,
            //        Description = influencer.Description,
            //        Email = User.GetEmail(),
            //        PhoneNumber = influencer.PhoneNumber,
            //        InstagramUrl = influencer.InstagramUrl,
            //        FacebookUrl = influencer.FacebookUrl,
            //        TwitterUrl = influencer.TwitterUrl,
            //        YouTubeUrl = influencer.YouTubeUrl,
            //        TikTokUrl = influencer.TikTokUrl,
            //        Photo = influencer.Photo,
            //        WebsiteUrl = influencer.WebSiteUrl,
            //        UserId = User.GetId()
            //    };

            //    return RedirectToAction("BecomePublisher", "Publishers", new { area = "" });
            //}

            var publisherId = this.publishers.IdByUser(this.User.GetId());
            byte[] imageBytes = null;
            if (photo != null)
            {
                if (photo.Length > 5 * 1024 * 1024)
                {
                    this.ModelState.AddModelError("Photo", "Image is too big. Max size is 5MB");
                }

                var imageInMemory = new MemoryStream();
                photo.CopyTo(imageInMemory);
                imageBytes = imageInMemory.ToArray();
            }
            else
            {
                var file = Path.GetFullPath(@"wwwroot\pics\noimage.jpg");
                imageBytes = System.IO.File.ReadAllBytes(file);                
            }

            if (!this.data.Countries.Any(x => x.Id == publisher.CountryId))
            {
                this.ModelState.AddModelError(nameof(publisher.CountryId), "Country do not exist");
            }

            if (!this.data.Genders.Any(x => x.Id == publisher.GenderId))
            {
                this.ModelState.AddModelError(nameof(publisher.GenderId), "Gender do not exist");
            }
            if(this.data.Publishers.Any(x => x.Username == publisher.Username))
            {
                this.ModelState.AddModelError(nameof(publisher.Username), "Username is already taken");
            }
            if(this.data.Publishers.Any(x => x.Email == User.GetEmail()))
            {
                this.ModelState.AddModelError(nameof(publisher.Email), "This email already exist");
            }
            if (publisherId != 0)
            {
                this.ModelState.AddModelError(nameof(publisher), "This Publisher already exist");
            }

            if (!ModelState.IsValid)
            {
                publisher.Conutries = this.getCollection.GetCountries();
                publisher.Genders = this.getCollection.GetGender();

                return View(publisher);
            }

            var publisherData = new Publisher
            {
                FirstName = publisher.FirstName,
                MiddleName = publisher.MiddleName,
                LastName = publisher.LastName,
                GenderId = publisher.GenderId,
                Username = publisher.Username,
                CountryId = publisher.CountryId,
                Description = publisher.Description,
                PhoneNumber = publisher.PhoneNumber,
                InstagramUrl = publisher.InstagramUrl,
                FacebookUrl = publisher.FacebookUrl,
                TwitterUrl = publisher.TwitterUrl,
                YouTubeUrl = publisher.YouTubeUrl,
                TikTokUrl = publisher.TikTokUrl,
                Email = publisher.Email,
                WebsiteUrl = publisher.WebSiteUrl,
                Photo = imageBytes,
                UserId = User.GetId()                
            };

            this.data.Publishers.Add(publisherData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(Publishers));
        }

        [Authorize]
        public IActionResult Publishers([FromQuery] AllPublishersQueryModel query)
        {
            var publishersQuery = this.data.Publishers.AsQueryable();
        
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                publishersQuery = publishersQuery.Where(i =>
                   (i.FirstName + " " + i.LastName).ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.FirstName.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.Username.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.LastName.ToLower().Contains(query.SearchTerm.ToLower()));
            }
        
            publishersQuery = query.Sorting switch
            {
                PublisherSorting.Username => publishersQuery.OrderBy(i => i.Username),
                PublisherSorting.FirstName => publishersQuery.OrderBy(i => i.FirstName),
                PublisherSorting.DateCreated or _ => publishersQuery.OrderByDescending(i => i.Id),
            };
        
            var totalPublishers = publishersQuery.Count();
        
            var publishers = publishersQuery
                .Skip((query.CurrentPage - 1) * AllPublishersQueryModel.PublishersPerPage)
                .Take(AllPublishersQueryModel.PublishersPerPage)
                .Select(i => new PublisherListingViewModel
                {
                    Id = i.Id,
                    Username = i.Username,
                    Country = i.Country.Name,
                    Photo = i.Photo                   
                })
                .ToList();
        
            query.TotalPublishers = totalPublishers;
            query.Publishers = publishers;
        
            return this.View(query);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var selected = this.data.Publishers
                .Where(p => p.Id == id)
                .Select(x => new PublisherViewModel
                {
                    Id = x.Id,
                    Photo = x.Photo,
                    Username = x.Username,
                    FacebookUrl = x.FacebookUrl,
                    InstagramUrl = x.InstagramUrl,
                    TwitterUrl = x.TwitterUrl,
                    CountryName = x.Country.Name,
                    Gender = x.Gender.Name,
                    Email = x.Email,
                    Offers = this.data.Offers.Where(x => x.PublisherId == id).ToList()
                })
                .FirstOrDefault();

            return this.View(selected);
        }
    }
}
