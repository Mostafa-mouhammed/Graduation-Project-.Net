using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.Users;
using Project.BL.Services.UnitService;
using System.Threading.Tasks;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitService _unit;

        public UserController(IUnitService unit)
        {
            _unit = unit;
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            StatuscodeDTO result = await _unit.user.getUser(User);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(SignupDTO user)
        {
            StatuscodeDTO result = await _unit.auth.registration(user);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            StatuscodeDTO result = await _unit.auth.Login(login);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO update)
        {
            StatuscodeDTO result = await _unit.user.updateUser(User, update);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateImage")]
        public async Task<IActionResult> UpdateImage(IFormFile image)
        {
            StatuscodeDTO result = await _unit.user.updateProfileImage(User, image);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UserPasswordUpdateDTO password)
        {
            StatuscodeDTO result = await _unit.user.updatePassword(User, password);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            StatuscodeDTO result = await _unit.user.GetAllUsers();
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            StatuscodeDTO result = await _unit.user.GetUserById(id);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            StatuscodeDTO result = await _unit.user.DeleteUser(id);
            return StatusCode((int)result.Statuscode, result.data ?? result.message);
        }
    }
}
