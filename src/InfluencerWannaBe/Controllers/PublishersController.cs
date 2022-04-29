namespace InfluencerWannaBe.Controllers
{
    using System;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authorization;

    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Infrastructure;
    using InfluencerWannaBe.Models.Publishers;
    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBe.Services.Publisher;
    using InfluencerWannaBe.Services.Influencers;

    public class PublishersController : Controller
    {
        private readonly IPublisherService publishers;
        private readonly IInfluencerService influencers;
        private readonly InfluencerWannaBeDbContext data;
        private readonly IGetCollection getCollection;
        private readonly IOfferService offerService;

        public PublishersController(InfluencerWannaBeDbContext data, IPublisherService publishers, IInfluencerService influencerService, IGetCollection getCollection, IOfferService offerService)
        {
            this.data = data;
            this.publishers = publishers;
            this.getCollection = getCollection;
            this.offerService = offerService;
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
            try
            {
                var influencerId = this.influencers.IdByUser(this.User.GetId());
                publisher.Email = User.GetEmail();

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
                if (this.data.Publishers.Any(x => x.Username == publisher.Username))
                {
                    this.ModelState.AddModelError(nameof(publisher.Username), "Username is already taken");
                }
                if (this.data.Publishers.Any(x => x.Email == User.GetEmail()))
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
            }
            catch(Exception ex)
            {
                Helper.Logs($"Error: {ex}" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), "PublisherController_BecomePublisher" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }
            return RedirectToAction(nameof(Publishers));
        }

        [Authorize]
        public IActionResult Publishers([FromQuery] AllPublishersQueryModel query)
        {
            try
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

                query.TotalElements = totalPublishers;
                query.ModelCollection = publishers;
            }
            catch(Exception ex)
            {
                Helper.Logs($"Error: {ex}" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), "PublisherController_Publishers" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }

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

        [Authorize]
        public IActionResult PublisherOffer()
        {
            var offers = this.offerService.OffersByUser(this.User.GetId());
            return this.View(offers);
        }

        [Authorize]
        public IActionResult AsignedInfluencers(int id)
        {
            var influencers = this.data.InfleuncerOffers
                .Where(x => x.OfferId == id)
                .Select(x => new InfluencerListingViewModel
                {
                   Id = x.InfluencerId,
                   Photo = x.Influencer.Photo,
                   Facebook = x.Influencer.FacebookUrl,
                   Instagram = x.Influencer.InstagramUrl,
                   Username = x.Influencer.Username,
                   AcceptedForTheOffer = x.AcceptedForTheOffer
                })
                .ToList();
            
            return this.View(influencers);
        }

        [Authorize]
        public IActionResult AcceptInfluencer(int id)
        {
            try
            {
                var influencerOffer = this.data.InfleuncerOffers.FirstOrDefault(x => x.InfluencerId == id);
                var influecer = this.data.Influencers.FirstOrDefault(x => x.Id == influencerOffer.InfluencerId);
                influencerOffer.AcceptedForTheOffer = true;
                this.data.InfleuncerOffers.Update(influencerOffer);
            }
            catch(Exception ex)
            {
                Helper.Logs($"Error: {ex}" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), "PublisherController_AcceptInfluencer" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }
            return RedirectToAction("AsignedInfluencers", "Publishers");
        }

        [Authorize]
        public IActionResult DeclineInfluencer(int id)
        {
            try
            {
                var influencerOffer = this.data.InfleuncerOffers.FirstOrDefault(x => x.InfluencerId == id);
                influencerOffer.AcceptedForTheOffer = false;
            }
            catch(Exception ex)
            {
                Helper.Logs($"Error: {ex}" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), "PublisherController_DeclineInfluencer" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }

            return RedirectToAction("AsignedInfluencers", "Publishers");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                this.publishers.DeletePublisherById(id);
            }
            catch(Exception ex)
            {
                Helper.Logs($"Error: {ex}" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), "PublisherController_Delete" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }

            if (User.IsAdmin())
            {
                return RedirectToAction("Publishers", "Publishers");
            }
            else
            {
                return RedirectToAction(nameof(PublishersController.Publishers));
            }
        }
    }
}
