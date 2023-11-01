using Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
   public interface IReviewCollection
    {

        Task<object> GetAllReviews();

        Task<List<Review>> GetReviewById(string id);

        Task InsertReview(Review Review);
        Task<bool> DeleteReview(string id);
    }
}
