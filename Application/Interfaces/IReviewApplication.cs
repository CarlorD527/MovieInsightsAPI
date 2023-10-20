using Application.Commons;
using Application.Dtos.Review;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IReviewApplication
    {
        Task<BaseResponse<List<Review>>> GetAllReview();
        Task<BaseResponse<List<Review>>> GetByIdReview(string id);
        Task<BaseResponse<bool>> addReview(InsertReviewDto movieDto);
        Task<BaseResponse<bool>> deleteReview(string id);
    }
}
