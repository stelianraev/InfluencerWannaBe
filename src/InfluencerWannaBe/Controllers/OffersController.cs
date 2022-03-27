﻿namespace InfluencerWannaBe.Controllers
{
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Infrastructure;
    using InfluencerWannaBe.Models.Offers;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Publisher;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Linq;

    public class OffersController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;
        private readonly IPublisherService publisherService;
        private readonly IGetCollection getCollection;

        public OffersController(InfluencerWannaBeDbContext data, IPublisherService publisherService, IGetCollection getCollection)
        {
            this.data = data;
            this.getCollection = getCollection;
            this.publisherService = publisherService;
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
                   i.SignUpInfluencers.Any(x => x.Username == query.SearchTerm.ToLower()));
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
                .Skip((query.CurrentPage - 1) * AllOffersQueryModel.OffersPerPage)
                .Take(AllOffersQueryModel.OffersPerPage)
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
        public IActionResult AddOffer()
        {
            var publisher = this.publisherService.IsPublisher(this.User.GetId());

            if (!publisher)
            {
               return RedirectToAction("BecomePublisher", "Publishers", new { area = "" });
            }

            return View(new OffersRegistrationFormModel
            {
                Conutries = this.getCollection.GetCountries(),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddOffer(OffersRegistrationFormModel offer, IFormFile photo)
        {
            var publisher = this.publisherService.IsPublisher(this.User.GetId());

            if (photo == null || photo.Length > 5 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Photo", "Image is too big. Max size is 5MB");
            }

            if (!this.data.Countries.Any(x => x.Id == offer.CountryId))
            {
                this.ModelState.AddModelError(nameof(offer.CountryId), "Country do not exist");
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
            var offerOwner = this.data.Publishers.FirstOrDefault(x => x.UserId == offerData.OwnerId);
            offerOwner.Offers.Add(offerData);
            this.data.Publishers.Update(offerOwner);
            this.data.SaveChanges();

            return RedirectToAction(nameof(Offers));
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var selected = this.data.Offers
                .Where(x => x.Id == id)
                .Select(x => new OfferViewModel
                {
                    Title = x.Title,
                    CountryId = x.CountryId,
                    Requirements = x.Requirents,
                    Description = x.Description,
                    CountryName = x.Country.Name,
                    Photo = x.Photo
                })
                .FirstOrDefault();

            return this.View(selected);
        }
    }
}