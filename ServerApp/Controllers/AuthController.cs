using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ServerApp.Data.DTOs;
using ServerApp.Data.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("register")] //<host>/api/auth/register
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        { //Data Transfer Object containing username and password.
            // validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower(); //Convert username to lower case before storing in database.

            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username is already taken");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username,
                UserGroupId = userForRegisterDto.Group
            };

            await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForRegisterDto)
        {
            var userFromRepo = await _repo.Login(userForRegisterDto.Username.ToLower(), userForRegisterDto.Password);
            if (userFromRepo == null) //User login failed
                return Unauthorized();

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var value = _config.GetSection("AppSettings:Token").Value;
            if (value != null)
            {
                var key = Encoding.ASCII.GetBytes(value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                        new Claim(ClaimTypes.Name, userFromRepo.Username)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { tokenString });
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Gets available user groups
        /// </summary>
        [HttpGet("get-user-groups")] //<host>/api/auth/get-user-groups
        public async Task<IActionResult> UserGroups()
        {
            var groups = await _repo.UserGroups();
            return Ok(groups);
        }
    }
}
