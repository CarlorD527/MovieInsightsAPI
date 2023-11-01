using Application.Commons;
using Application.Dtos.Review;
using Domain.Models;
using MongoDB.Bson;
using System.Text.Json.Nodes;

namespace Application.Interfaces
{
    public interface IReviewApplication
    {
        //Task<BaseResponse<List<object>>> GetAllReview();
        Task<string> GetAllReview();
        Task<BaseResponse<List<Review>>> GetByIdReview(string id);
        Task<BaseResponse<bool>> addReview(InsertReviewDto movieDto);
        Task<BaseResponse<bool>> deleteReview(string id);
    }
}
