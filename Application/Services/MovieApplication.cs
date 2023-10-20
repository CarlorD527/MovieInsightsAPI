using Application.Commons;
using Application.Dtos.Movie;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories;
using MongoDB.Driver.Core.WireProtocol.Messages;
using System.Net;

namespace Application.Services
{
    public class MovieApplication : IMovieApplication
    {
        private readonly MovieCollection _movieCollection;

        private readonly IMapper _mapper;

        private readonly MovieValidators _validatorRules;

        public MovieApplication(MovieCollection movieCollection, MovieValidators validatorRules, IMapper mapper)
        {
            _movieCollection = movieCollection;
            _mapper = mapper;
            _validatorRules = validatorRules;
        }

        public async Task<BaseResponse<bool>> addMovie(InsertMovieDto movieDto)
        {
            var response = new BaseResponse<bool>();

            var validationResult = await _validatorRules.ValidateAsync(movieDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.ValidationErrors = validationResult.Errors;

            }
            else {
                try
                {
                    //Mapeo del postDto al post
                    var movie = _mapper.Map<Movie>(movieDto);
                    movie.DatedCreated = DateTime.UtcNow;
                    movie.State = "Active";

                    // Intenta insertar la película
                    await _movieCollection.InsertMovie(movie);

                    response.Data = false;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;

                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string>{ex.Message};
                }
            }
    
            return response;
        }

        public async Task<BaseResponse<bool>> deleteMovie(string id)
        {
            var response = new BaseResponse<bool>();

            try
            {
                bool isDeleted = await _movieCollection.DeleteMovie(id);

                if (isDeleted)
                {
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { "No se pudo eliminar la película." };
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

        public async Task<BaseResponse<List<Movie>>> GetAllMovies()
        {
            var response = new BaseResponse<List<Movie>>();

            try
            {
                var movies = await _movieCollection.GetAllMovies();

                if (movies is not null)
                {
                    response.IsSuccess = true;
                    response.Data = movies;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = null;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { "No se pudieron recuperar las películas." };
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

        public async Task<BaseResponse<List<Movie>>> GetByIdMovie(string id)
        {
            var response = new BaseResponse<List<Movie>>();

            try
            {
                var movie = await _movieCollection.GetMovieById(id);

                if (movie is not null)
                {
                    response.IsSuccess = true;
                    response.Data = movie;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { "No se pudo encontrar la película con el ID especificado." };
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
    }
}
