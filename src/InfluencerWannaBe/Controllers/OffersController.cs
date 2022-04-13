namespace InfluencerWannaBe.Controllers
{
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;
    using InfluencerWannaBe.Infrastructure;
    using InfluencerWannaBe.Models.Offers;
    using InfluencerWannaBe.Services;
    using InfluencerWannaBe.Services.Offers;
    using InfluencerWannaBe.Services.Publisher;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.IO;
    using System.Linq;

    public class OffersController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;
        private readonly IPublisherService publisherService;
        private readonly IGetCollection getCollection;
        private readonly IOfferService offerService;

        public OffersController(InfluencerWannaBeDbContext data, IPublisherService publisherService, IGetCollection getCollection, IOfferService offerService)
        {
            this.data = data;
            this.getCollection = getCollection;
            this.offerService = offerService;
            this.publisherService = publisherService;
        }

        [Authorize]
        public IActionResult Offers([FromQuery] AllOffersQueryModel query, int id)
        {
            var offersQuery = this.data.Offers.AsQueryable();

            if (id != 0) 
            {
               offersQuery = this.data.Offers.Where(x => x.PublisherId == id).AsQueryable();
            }          

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                offersQuery = offersQuery.Where(i =>
                   i.Title.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.Publisher.Username.ToLower().Contains(query.SearchTerm.ToLower()) ||
                   i.SignUpInfluencers.Any(x => x.Influencer.Username == query.SearchTerm.ToLower()));
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
                .Where(x => x.IsExpired == false)
                .Skip((query.CurrentPage - 1) * AllOffersQueryModel.OffersPerPage)
                .Take(AllOffersQueryModel.OffersPerPage)
                .Select(i => new OffersListingViewModel
                {
                    Id = i.Id,
                    Title = i.Title,
                    Payment = i.Payment,
                    OfferId = i.Id,
                    PublisherUserName = i.Publisher.Username,
                    Country = i.Country.Name,
                    Photo = i.Photo,
                    Influencers = i.SignUpInfluencers,
                    ExpireDate = i.ExpireDate,
                    CreationDate = i.CreationDate,
                    Update = i.Update,
                    IsExpired = i.IsExpired
                })
                .ToList();

            query.TotalElements = totalOffers;
            query.ModelCollection = offers;

            return this.View(query);
        }


        [Authorize]
        public IActionResult Delete(int id)
        {
            this.offerService.DeleteOfferById(id);
            if (User.IsAdmin())
            {
                return RedirectToAction("Offers", "Offers");
            }
            else
            {
            return RedirectToAction(nameof(PublishersController.PublisherOffer));
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(OffersRegistrationFormModel offer, IFormFile photo, int id)
        {
           var selectedOffer = this.offerService.GetOffer(id);

            if (photo != null)
            {
                if (photo.Length > 5 * 1024 * 1024)
                {
                    this.ModelState.AddModelError("Photo", "Image is too big. Max size is 5MB");
                }

                var imageInMemory = new MemoryStream();
                photo.CopyTo(imageInMemory);
                var imageBytes = imageInMemory.ToArray();
                selectedOffer.Photo = imageBytes;
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

            selectedOffer.Title = offer.Title;
            selectedOffer.CountryId = offer.CountryId;
            selectedOffer.Description = offer.Description;
            selectedOffer.Requirents = offer.Requirements;
            selectedOffer.OwnerId = User.GetId();
            selectedOffer.Payment = offer.Payment;
            selectedOffer.IsPossibleToSignIn = true;
            selectedOffer.Update = DateTime.Now;
            selectedOffer.ExpireDate = selectedOffer.CreationDate > selectedOffer.Update ? selectedOffer.CreationDate.AddDays(30) : selectedOffer.Update.AddDays(30);



            //var offerOwner = this.data.Publishers.FirstOrDefault(x => x.UserId == offerData.OwnerId);
            //offerOwner.Offers.Add(offerData);
            //this.data.Publishers.Update(offerOwner);
            this.data.Offers.Update(selectedOffer);
            this.data.SaveChanges();
            if (User.IsAdmin())
            {
                return RedirectToAction("Offers", "Offers");
            }
            else
            {
            return RedirectToAction(nameof(PublishersController.PublisherOffer));                
            }
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
           var offer = this.offerService.GetOffer(id);
            var offerReg = new OffersRegistrationFormModel()
            {
                Title = offer.Title,
                CountryId = offer.CountryId,
                Description = offer.Description,
                Photo = offer.Photo,
                Requirements = offer.Requirents,
                OwnerId = offer.Id,
                Payment = offer.Payment,
                IsPossibleToSignIn = true,
                Conutries = this.getCollection.GetCountries(),

            };
           return this.View(offerReg);
        }

        [Authorize]
        public IActionResult Remove(int id)
        {
            var offer = this.offerService.GetOffer(id);

            var influencer = this.data
                .Influencers
                .FirstOrDefault(x => x.UserId == this.User.GetId());

            var influencerOffer = this.data.InfleuncerOffers.FirstOrDefault(x => x.InfluencerId == influencer.Id && x.OfferId == offer.Id);

            this.data.InfleuncerOffers.Remove(influencerOffer);
            this.data.SaveChanges();

            return RedirectToAction("SignInOffers", "Influencers"); //nameof(InfluencersController.SignInOffers));            
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

            var offerData = new Offer
            {
                Title = offer.Title,
                CountryId = offer.CountryId,
                Description = offer.Description,
                Photo = imageBytes,
                Requirents = offer.Requirements,
                OwnerId = User.GetId(),
                Payment = offer.Payment,
                IsPossibleToSignIn = true,
                CreationDate = DateTime.Now,
                Update = default,
                ExpireDate = offer.CreationDate.AddDays(30),
                IsExpired = false
            };

            this.data.Offers.Add(offerData);
            var offerOwner = this.data.Publishers.FirstOrDefault(x => x.UserId == offerData.OwnerId);
            offerOwner.Offers.Add(offerData);
            //this.data.Publishers.Update(offerOwner);
            this.data.SaveChanges();

            return RedirectToAction(nameof(Offers));
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var selected = this.data.Offers
                .Where(o => o.Id == id)
                .Select(x => new OfferViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CountryId = x.CountryId,
                    Requirements = x.Requirents,
                    Description = x.Description,
                    CountryName = x.Country.Name,
                    Payment = x.Payment,
                    Photo = x.Photo,
                    AssignedInfluencers = x.SignUpInfluencers,
                    Update = x.Update,
                    ExpireDate = x.ExpireDate,
                    CreationDate = x.CreationDate                    
                })
                .FirstOrDefault();

            return this.View(selected);
        }

        public IActionResult SignUp(int id)
        {
            var influencer = this.data.Influencers.FirstOrDefault(x => x.UserId == this.User.GetId());
            var offer = this.data.Offers.FirstOrDefault(x => x.Id == id);
            InfluencerOffers infOff = new InfluencerOffers();

            infOff.Influencer = influencer;
            infOff.Offer = offer;
            infOff.AcceptedForTheOffer = null;

            this.data.InfleuncerOffers.Add(infOff);

            //offer.SignUpInfluencers.Add(influencer);
            //influencer.SignUpOffers.Add(offer);

            this.data.Influencers.Update(influencer);
            this.data.Offers.Update(offer);

            this.data.SaveChanges();

            return RedirectToAction(nameof(Offers));
        }
    }
}
