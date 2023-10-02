using AuthJWT.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthJWT.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "darius",
                LastName = "black",
                Username = "admin",
                Password = "1234",
                Role = "Admin",
            },
            new User
            {
                Id = 2,
                FirstName = "david",
                LastName = "black",
                Username = "user",
                Password = "1234",
                Role = "User",
            },
        };

        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;
        }


        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username
                && x.Password == password);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Secret").Value);
            var claims = new ClaimsIdentity();
            claims.AddClaims(new[]
            {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            });

            var tokenDescriptore = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptore);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.ToList();
        }
    }
}
