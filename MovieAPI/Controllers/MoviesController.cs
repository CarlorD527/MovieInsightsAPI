using Application.Dtos.Movie;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieApplication _movieApplication;

        public MoviesController(IMovieApplication moviesApplication)
        {

            _movieApplication = moviesApplication;
        }


        [HttpGet("Get")]
        public async Task<ActionResult> listMovies()
        {

            var response = await _movieApplication.GetAllMovies();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMovieById(string id)
        {
            var movie = await _movieApplication.GetByIdMovie(id);

            if (movie != null)
            {
                return Ok(movie);
            }

            return NotFound();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterMovie(InsertMovieDto requestDto)

        {
            var response = await _movieApplication.addMovie(requestDto);

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovieById(string id)
        {
            var deleteResponse = await _movieApplication.deleteMovie(id);


            return Ok(deleteResponse);
        }

    }
}
