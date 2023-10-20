using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IMovieCollection
    {
        Task<List<Movie>> GetAllMovies();

        Task<List<Movie>> GetMovieById (string id);

        Task InsertMovie(Movie movie);
        Task<bool> DeleteMovie(string id);
        
    }
}
