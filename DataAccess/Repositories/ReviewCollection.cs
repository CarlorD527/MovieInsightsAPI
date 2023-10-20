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

        private IMongoCollection<Review> Collection;

        public ReviewCollection(IOptions<MovieDatabaseSettings> settings)
        {
   

            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            var peliculasCollection = mongoDb.GetCollection<Movie>("Movie");
            var usuariosCollection = mongoDb.GetCollection<User>("Users");


            Collection = mongoDb.GetCollection<Review>("Review");
        }

        public async Task<bool> DeleteReview(string id)
        {
            try
            {
                var filter = Builders<Review>.Filter.Eq(s => s.Id, id);
                var result = await Collection.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Review>> GetAllReviews()
        {


            var pipeline = new BsonDocument[]
              {
                    // Realizar un join con la colección de películas
                    new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "Movies" }, // Nombre de la colección a unir
                        { "localField", "MovieId" }, // Campo local para hacer coincidir
                        { "foreignField", "_id" }, // Campo en la colección de películas para hacer coincidir
                        { "as", "movieTitle" } // Nombre del campo en el resultado
                    }),

                    // Realizar un join con la colección de usuarios
                    new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "Users" }, // Nombre de la colección a unir
                        { "localField", "UserId" }, // Campo local para hacer coincidir
                        { "foreignField", "_id" }, // Campo en la colección de usuarios para hacer coincidir
                        { "as", "username" } // Nombre del campo en el resultado
                    }),

                    // Proyectar los campos deseados
                    new BsonDocument("$project", new BsonDocument
                    {
                        { "_id", 1 }, // Incluir el ID de la reseña
                        { "ReviewContent", 1 }, // Incluir el contenido de la reseña
                        { "movie.Title", 1 }, // Incluir el título de la película
                        { "user.Username", 1 } // Incluir el nombre de usuario
                    })
              };
            var result = await Collection.Aggregate<Review>(pipeline).ToListAsync();
            return result;
        }

        public async Task<List<Review>> GetReviewById(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.ToListAsync();

        }

        public async Task InsertReview(Review review)
        {

            await Collection.InsertOneAsync(review);

        }
    }
}
