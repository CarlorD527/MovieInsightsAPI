using Application.Dtos.Review;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReviewAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewApplication _reviewApplication;

        public ReviewController(IReviewApplication reviewsApplication)
        {

            _reviewApplication = reviewsApplication;
        }


        [HttpGet("Get")]
        public async Task<ActionResult> listReview()
        {

                var response = await _reviewApplication.GetAllReview();

                return Ok(response);
         

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetReviewById(string id)
        {
            var review = await _reviewApplication.GetByIdReview(id);

            if (review != null)
            {
                return Ok(review);
            }

            return NotFound();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterReview(InsertReviewDto requestDto)

        {
            var response = await _reviewApplication.addReview(requestDto);

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReviewById(string id)
        {
            var deleteResponse = await _reviewApplication.deleteReview(id);


            return Ok(deleteResponse);
        }
    }
}
