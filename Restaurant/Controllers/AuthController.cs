using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models.DTOs;
using Restaurant.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RestaurantContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(RestaurantContext context, IConfiguration configuration)
        {
           _context = context;
            _configuration = configuration;
        }

        // Register a new user and assign Admin role
        [HttpPost("Register")]
        public async Task <IActionResult> Register(RegisterDTO registerUser)
        {
            var exitingUser = await _context.Auths.SingleOrDefaultAsync(u => u.Email == registerUser.Email);


            if (exitingUser != null) 
            {
                return BadRequest("Email is already in use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);

            var newAuth = new Auth
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                PasswordHash = passwordHash
            };

            _context.Auths.Add(newAuth);
            _context.SaveChanges();

            return Ok();           
        }

        // Log in a user and generate a JWT token
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginUser)
        {
            //Check if User(Admin) is in Database 
            var user =await _context.Auths.SingleOrDefaultAsync(u => u.Email == loginUser.Email);

            //Confirm this Password is match with the passwoer to exit in Database
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }


            //When user is logged in ,generate the token
            var token = GenerateJwtToken(user);
            return Ok(new {token});
        }
        private string GenerateJwtToken(Auth auth)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];


            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, $"{auth.FirstName}{auth.LastName}"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, auth.Email),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),               
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
