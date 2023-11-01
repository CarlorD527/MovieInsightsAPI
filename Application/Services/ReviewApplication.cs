using Application.Commons;
using Application.Dtos.Review;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;

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

        public async Task<string> GetAllReview()
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            //var response = new BaseResponse<List<object>>();
            var  reviews = await _reviewCollection.GetAllReviews();

            var reviewList = reviews.ToJson();

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(reviewList);

            return JsonSerializer.Serialize(jsonElement, options);

        }

        public Task<BaseResponse<List<Review>>> GetByIdReview(string id)
        {
            throw new NotImplementedException();
        }
    }
}
