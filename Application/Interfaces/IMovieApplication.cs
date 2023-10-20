using Application.Commons;
using Application.Dtos.Movie;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMovieApplication
    {
        Task<BaseResponse<List<Movie>>> GetAllMovies();
        Task<BaseResponse<List<Movie>>> GetByIdMovie(string id);
        Task<BaseResponse<bool>> addMovie(InsertMovieDto movieDto);
        Task<BaseResponse<bool>> deleteMovie(string id);
    }
}
