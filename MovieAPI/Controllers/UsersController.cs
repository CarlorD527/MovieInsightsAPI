using Application.Dtos.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UsersController(IUserApplication usersApplication)
        {

            _userApplication = usersApplication;
        }


        [HttpGet("Get")]
        public async Task<ActionResult> listUsers()
        {

            var response = await _userApplication.GetAllUsers();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdUser(string id)
        {
            var user = await _userApplication.GetByIdUser(id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(InsertUserDto requestDto)

        {
            var response = await _userApplication.addUser(requestDto);

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserById(string id)
        {
            var deleteResponse = await _userApplication.deleteUser(id);


            return Ok(deleteResponse);
        }

    }
}
