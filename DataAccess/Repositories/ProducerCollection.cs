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
    public class ProducerCollection : IProducerCollection
    {

        internal MovieDatabaseSettings _config = new MovieDatabaseSettings();

        private IMongoCollection<Producer> Collection;

        public ProducerCollection(IOptions<MovieDatabaseSettings> producerDatabaseSettings)
        {



            var mongoClient = new MongoClient(producerDatabaseSettings.Value.ConnectionString);


            var mongoDatabase = mongoClient.GetDatabase(producerDatabaseSettings.Value.DatabaseName);

            Collection = mongoDatabase.GetCollection<Producer>("Producers");

        }

        public async Task<bool> DeleteProducer(string id)
        {
            try
            {
                var filter = Builders<Producer>.Filter.Eq(s => s.Id, id);
                var result = await Collection.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public Task<List<Producer>> GetAllProducer()
        {
            throw new NotImplementedException();
        }

        public Task<List<Producer>> GetAllProducerById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Producer>> GetAllProducers()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<List<Producer>> GetProducerById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.ToListAsync();

        }

        public async Task InsertProducer(Producer producer)
        {

            await Collection.InsertOneAsync(producer);

        }
    }
}
