using Microsoft.Extensions.Configuration; // Add this line
using PersonalTrainerAPI.Models;
using System.Threading.Tasks;
using System.Security.Claims; // For Claim
using System.Text; // For Encoding
using Microsoft.AspNetCore.Authentication.JwtBearer; // For JwtBearerDefaults
using Microsoft.IdentityModel.Tokens; // For JwtSecurityToken and JwtSecurityTokenHandler
using Microsoft.AspNetCore.Authorization; // For Authorize attribute
using Microsoft.AspNetCore.Mvc; // For ControllerBase
using Microsoft.AspNetCore.Identity; // For Identity features
using System.IdentityModel.Tokens.Jwt; // For JwtSecurityToken and JwtSecurityTokenHandler

namespace PersonalTrainerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration; 

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration; // Assigning the injected configuration
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully." });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid login attempt." });
            }

            // Check the password
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized(new { message = "Invalid login attempt." });
            }

            // Create the claims for the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Generate the JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            // Return the token to the client
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = DateTime.Now.AddMinutes(30) // Optional: you can return the token's expiration time
            });
        }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
