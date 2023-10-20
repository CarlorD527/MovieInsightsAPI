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
    public class MovieCollection : IMovieCollection
    {

        internal MovieDatabaseSettings _config = new MovieDatabaseSettings();

        private IMongoCollection<Movie> Collection;

        public MovieCollection(IOptions<MovieDatabaseSettings> movieDatabaseSettings) {

        

            var mongoClient = new MongoClient(movieDatabaseSettings.Value.ConnectionString);

            
            var mongoDatabase = mongoClient.GetDatabase(movieDatabaseSettings.Value.DatabaseName);
            
            Collection = mongoDatabase.GetCollection<Movie>("Movies");

        }

        public async Task<bool> DeleteMovie(string id)
        {
            try
            {
                var filter = Builders<Movie>.Filter.Eq(s => s.Id,id);
                var result = await Collection.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<List<Movie>> GetMovieById(string id)
        {
            return await Collection.FindAsync(new BsonDocument {{ "_id" , new ObjectId(id) }}).Result.ToListAsync();     
                
                }

        public async Task InsertMovie(Movie movie)
        {
                
                await Collection.InsertOneAsync(movie);
  
        }
    }
}
