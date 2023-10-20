using Application.Commons;
using Application.Dtos.Review;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MongoDB.Bson;
using System.Net;
using System.Security.Cryptography;

namespace Application.Services
{
    public class ReviewApplication : IReviewApplication
    {
        private readonly ReviewCollection _reviewCollection;

        private readonly IMapper _mapper;

        private readonly ReviewValidators _validatorRules;

        public ReviewApplication(ReviewCollection reviewCollection, ReviewValidators validatorRules, IMapper mapper)
        {
            _reviewCollection = reviewCollection;
            _mapper = mapper;
            _validatorRules = validatorRules;
        }
        public async Task<BaseResponse<bool>> addReview(InsertReviewDto reviewDto)
        {
            var response = new BaseResponse<bool>();

            var validationResult = await _validatorRules.ValidateAsync(reviewDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.ValidationErrors = validationResult.Errors;

            }
            else
            {
                try
                {

                    //Mapeo del reviewDto al review
                    var review = _mapper.Map<Review>(reviewDto);
                    review.DatedCreated = DateTime.UtcNow;
                    review.State = "Active";

                    // Intenta insertar el review
                    await _reviewCollection.InsertReview(review);

                    response.Data = false;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;

                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { ex.Message };
                }
            }

            return response;
        }

        public Task<BaseResponse<bool>> deleteReview(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<Review>>> GetAllReview()
        {
            var response = new BaseResponse<List<Review>>();

            try
            {
                var reviews = await _reviewCollection.GetAllReviews();

                if (reviews is not null)
                {
                    response.IsSuccess = true;
                    response.Data = reviews;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = null;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { "No se pudieron recuperar los reviews." };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ApplicationErrors = new List<string> { "Error de la aplicación: " + ex.Message };
            }

            return response;
        }

        public Task<BaseResponse<List<Review>>> GetByIdReview(string id)
        {
            throw new NotImplementedException();
        }
    }
}
