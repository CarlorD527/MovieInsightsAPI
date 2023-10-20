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
    public class UserCollection : IUserCollection
    {

        internal MovieDatabaseSettings _config = new MovieDatabaseSettings();

        private IMongoCollection<User> Collection;

        public UserCollection(IOptions<MovieDatabaseSettings> userDatabaseSettings)
        {

            var mongoClient = new MongoClient(userDatabaseSettings.Value.ConnectionString);


            var mongoDatabase = mongoClient.GetDatabase(userDatabaseSettings.Value.DatabaseName);

            Collection = mongoDatabase.GetCollection<User>("Users");

        }

        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(s => s.Id, id);
                var result = await Collection.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<List<User>> GetUserById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.ToListAsync();

        }

        public async Task InsertUser(User user)
        {

            await Collection.InsertOneAsync(user);

        }
    }
}
