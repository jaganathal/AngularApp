using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ClaimsAPI.Data;
using ClaimsAPI.Dtos;
using ClaimsAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClaimsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository, IConfiguration config)
        {
            _config = config;
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request
            if (await _authRepository.UserExists(userForRegisterDto.UserName))
            {
                return BadRequest("Username already exists");
            }
            var userToCreate = new User
            {
                Username = userForRegisterDto.UserName
            };
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userDetails = await _authRepository.Login(userForLoginDto.Username, userForLoginDto.Password);
            if (userDetails == null)
            {
                return Unauthorized();
            }
            var token = CreateJwtToken(userDetails);
            return Ok(new {token});
        }

        private string CreateJwtToken(User userDetails)
        {
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userDetails.Id.ToString()),
                new Claim(ClaimTypes.Name, userDetails.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor =  new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = cred
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token =  tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}