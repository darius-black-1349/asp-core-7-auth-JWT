using AuthJWT.DTOs;
using AuthJWT.Entity;
using AuthJWT.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace AuthJWT.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public User Login([FromBody] UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null) return null;

            return user;
        }
    }
}
