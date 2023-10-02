using AuthJWT.DTOs;
using AuthJWT.Entity;
using AuthJWT.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthJWT.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[Controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public User Login([FromBody] UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null) return null;

            return user;
        }

        [Authorize(Policy = "GetAllUser")]
        [HttpGet("all")]
        public IEnumerable<User> GetAllUsers()
        {
            return _userService.GetAll();
        }
    }
}
