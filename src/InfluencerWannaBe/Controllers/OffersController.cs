namespace InfluencerWannaBe.Controllers
{
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Infrastructure;
    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBe.Models.Offers;
    using InfluencerWannaBe.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Linq;

    public class OffersController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;
        private readonly IGetCollection getCollection;

        public OffersController(InfluencerWannaBeDbContext data, IGetCollection getCollection)
        {
            this.data = data;
            this.getCollection = getCollection;         
        }

        [Authorize]
        public IActionResult Offers([FromQuery] AllOffersQueryModel query)
        {
            var offersQuery = this.data.Offers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                offersQuery = offersQuery.Where(i =>
                   i.Title.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.Publisher.Username.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.Influencers.Any(x => x.Username == query.SearchTerm.ToLower()));
            }

            offersQuery = query.Sorting switch
            {
                OffersSorting.Title => offersQuery.OrderBy(o => o.Title),
                OffersSorting.Username => offersQuery.OrderBy(o => o.Publisher.Username),                
                OffersSorting.PaymentAZ => offersQuery.OrderBy(o => o.Payment),
                OffersSorting.PaymentZA => offersQuery.OrderByDescending(o => o.Payment),
                OffersSorting.Country => offersQuery.OrderBy(c => c.Country),
                OffersSorting.DateCreated or _ => offersQuery.OrderByDescending(i => i.Id),
            };

            var totalOffers = offersQuery.Count();

            var offers = offersQuery
                .Skip((query.CurrentPage - 1) * AllInfluencersQueryModel.InfluencersPerPage)
                .Take(AllInfluencersQueryModel.InfluencersPerPage)
                .Select(i => new OffersListingViewModel
                {
                    Id = i.Id,
                    Title = i.Title,
                    Payment = i.Payment,
                    PublisherUserName = i.Publisher.Username,
                    Country = i.Country.Name,
                    Photo = i.Photo
                })
                .ToList();

            query.TotalOffers = totalOffers;
            query.Offers = offers;

            return this.View(query);
        }

        [Authorize]
        public IActionResult AddOffer() => View(new OffersRegistrationFormModel
        {
            Conutries = this.getCollection.GetCountries(),          
        });

        [Authorize]
        [HttpPost]
        public IActionResult AddOffer(OffersRegistrationFormModel offer, IFormFile photo)
        {
            if (photo == null || photo.Length > 5 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Photo", "Image is too big. Max size is 5MB");
            }

            if (!this.data.Countries.Any(x => x.Id == offer.CountryId))
            {
                this.ModelState.AddModelError(nameof(offer.CountryId), "Country do not exist");
            }

            if (!ModelState.IsValid)
            {
                offer.Conutries = this.getCollection.GetCountries();
               
                return View(offer);
            }

            var imageInMemory = new MemoryStream();
            photo.CopyTo(imageInMemory);
            var imageBytes = imageInMemory.ToArray();

            var offerData = new Offer
            {
                Title = offer.Title,
                CountryId = offer.CountryId,
                Description = offer.Description,
                Photo = imageBytes,
                Requirents = offer.Requirements,
                OwnerId = User.GetId()
            };

            this.data.Offers.Add(offerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(Offers));
        }
    }
}
