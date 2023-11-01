using Domain.Models;
using Infrastructure.Interfaces;
using Infrastructure.MongoConfig;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReviewCollection : IReviewCollection
    {

        internal MovieDatabaseSettings _config = new MovieDatabaseSettings();

        private IMongoCollection<BsonDocument> Collection;

        private IMongoCollection<BsonDocument> moviesCollection;

        private IMongoCollection<BsonDocument> usersCollection;

        public ReviewCollection(IOptions<MovieDatabaseSettings> settings)
        {
   

            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);

            moviesCollection = mongoDb.GetCollection<BsonDocument>("Movie");
            usersCollection = mongoDb.GetCollection<BsonDocument>("Users");

            Collection = mongoDb.GetCollection<BsonDocument>("Review");

        }

        public Task<bool> DeleteReview(string id)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> DeleteReview(string id)
        ////{
        ////    try
        ////    {
        ////        var filter = Builders<Review>.Filter.Eq(s => s.Id, id);
        ////        var result = await Collection.DeleteOneAsync(filter);
        ////        return result.DeletedCount > 0;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Console.WriteLine($"Error: {ex.Message}");
        ////        return false;
        ////    }
        //}

        public async Task<object> GetAllReviews()
        {

            var result = from docA in Collection.AsQueryable()
                         join docB in usersCollection.AsQueryable()
                         on docA["UserId"] equals docB["UserId"]
                         //join docC in moviesCollection.AsQueryable()
                         //on docA["MovieId"] equals docC["MovieId"]
                         select new
                         {

                             username = docB["username"],
                             score = docA["score"],
                             reviewcontent = docA["reviewContent"],
                             datedcreated = docA["datedCreated"].ToString(),
                             //movietitle = docC["movieTitle"]
                         };

            return  result.ToList<object>();
        }

        public Task<List<Review>> GetReviewById(string id)
        {
            throw new NotImplementedException();
        }

        public Task InsertReview(Review Review)
        {
            throw new NotImplementedException();
        }

 
    }
}
