namespace InfluencerWannaBeUnitTest.Mocks
{
    using Microsoft.EntityFrameworkCore;

    using InfluencerWannaBe.Data;
    using InfluencerWannaBe.Data.Models;

    public static class DatabaseMock
    {
        public static InfluencerWannaBeDbContext Instance
        {
            get
            {
                var db = new DbContextOptionsBuilder<InfluencerWannaBeDbContext>()
                .UseInMemoryDatabase(databaseName: "InfluencerWannaBeDatabase")
                .Options;

                InfluencerWannaBeDbContext dbContext; 
                dbContext = new InfluencerWannaBeDbContext(db);
                dbContext.Countries.Add(new Country { Id = 2, Name = "Bulgaria" });
                dbContext.Countries.Add(new Country { Id = 3, Name = "France" });
                dbContext.Countries.Add(new Country { Id = 4, Name = "Portugal" });
                dbContext.Countries.Add(new Country { Id = 5, Name = "Spain" });
                dbContext.Countries.Add(new Country { Id = 6, Name = "Romania" });
                dbContext.SaveChanges();

                dbContext.Genders.Add(new Gender { Id = 1, Name = "Man" });
                dbContext.Genders.Add(new Gender { Id = 2, Name = "Woman" });
                dbContext.SaveChanges();

                dbContext.InfleuncerOffers.Add(new InfluencerOffers { Id = 1, InfluencerId = 2, OfferId = 2 });
                dbContext.InfleuncerOffers.Add(new InfluencerOffers { Id = 2, InfluencerId = 3, OfferId = 3 });
                dbContext.InfleuncerOffers.Add(new InfluencerOffers { Id = 3, InfluencerId = 4, OfferId = 4});
                dbContext.SaveChanges();

                dbContext.Publishers.Add(new Publisher { Id = 1, CountryId = 1, FirstName = "FirstName", LastName = "LastName", UserId = "testovUser" });
                dbContext.Publishers.Add(new Publisher { Id = 2, CountryId = 2, FirstName = "FirstName1", LastName = "LastName1", UserId = "test2" });
                dbContext.Publishers.Add(new Publisher { Id = 3, CountryId = 3, FirstName = "FirstName3", LastName = "LastName3", UserId = "test3" });
                dbContext.SaveChanges();

                dbContext.Influencers.Add(new Influencer { Age = 20, CountryId = 2, Email = "influencer1@gmail.com", Id = 1, UserId = "test1", Username = "infl1", FirstName = "test1", LastName = "test1lastname", });
                dbContext.Influencers.Add(new Influencer { Age = 21, CountryId = 5, Email = "influencer2@gmail.com", Id = 2, UserId = "testovUser", Username = "infl2", FirstName = "test2", LastName = "test2lastname" });
                dbContext.Influencers.Add(new Influencer { Age = 22, CountryId = 7, Email = "influencer3@gmail.com", Id = 3, UserId = "test3", Username = "infl3", FirstName = "test3", LastName = "test3lastname" });
                dbContext.Influencers.Add(new Influencer { Age = 23, CountryId = 8, Email = "influencer4@gmail.com", Id = 4, UserId = "test4", Username = "infl4", FirstName = "test4", LastName = "test4lastname" });
                dbContext.Influencers.Add(new Influencer { Age = 24, CountryId = 10, Email = "influencer5@gmail.com", Id = 5, UserId = "test5", Username = "infl5", FirstName = "test5", LastName = "test5lastname" });
                dbContext.Influencers.Add(new Influencer { Age = 25, CountryId = 22, Email = "influencer6@gmail.com", Id = 6, UserId = "test6", Username = "infl6", FirstName = "test6", LastName = "test6lastname" });
                dbContext.SaveChanges();

                dbContext.Offers.Add(new Offer { Id = 1, CountryId = 2, Title = "testing" });
                dbContext.Offers.Add(new Offer { Id = 2, CountryId = 3, Title = "testing3", OwnerId = "testovUser" });
                dbContext.SaveChanges();

                return dbContext;
            }
        }
    }
}
