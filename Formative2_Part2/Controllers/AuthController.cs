using Formative2_Part2.Data;
using Formative2_Part2.DTOs;
using Formative2_Part2.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace Formative2_Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase // controller for handling user authentication and registration
    {
        private readonly ApplicationDBcontext _context;

        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDBcontext context, IConfiguration configuration)
        {
            _context = context; // InjectS the database context for accessing user data
            _configuration = configuration;
        }

        /// <summary>
        /// Registers a new user. Expects user registration details in the body. Returns a success message.
        /// </summary>
        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);// using bcrypt to hash the password for security  God (2023)

            var newUser = new User
            { //The User entity values from  Users in the database are set to the values from the request DTO
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = passwordHash,
                UserTypeId = request.UserTypeId
            };

            _context.Users.Add(newUser);// when a new user is added save changes to the database with the save method
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        /// <summary>
        /// Authenticates a user and generates a JWT with login credentials that matches the stored registered user
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login(UserLoginDto request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Invalid email or password.");// If email and password do not match  a bad request response with an error message is returned
            }
            string token = CreateToken(user);
            return Ok(token); // If the email and password match a JWT token is created and returned in the response
        }

        private string CreateToken(User user) // Token creation method to generate a JWT token for the authenticated user
        {
            List<Claim> claims = new List<Claim>
                      {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserTypeId.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,

                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // returns the generated JWT token as a string 
        }
    }
}