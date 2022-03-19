namespace InfluencerWannaBe.Controllers
{
    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Models.Influencers;
    using InfluencerWannaBe.Models.Offers;
    using Microsoft.AspNetCore.Mvc;  
    using System.Linq;

    public class OffersController : Controller
    {
        private readonly InfluencerWannaBeDbContext data;

        public OffersController(InfluencerWannaBeDbContext data)
            => this.data = data;

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
                    Photo = i.Photo
                })
                .ToList();

            query.TotalOffers = totalOffers;
            query.Offers = offers;

            return this.View(query);
        }
    }
}
