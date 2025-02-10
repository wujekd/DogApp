using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

namespace DogApp.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountsController : Controller
{
    private readonly AppDbContext _DbContext;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;


    public AccountsController(AppDbContext dbContext, IConfiguration configuration, UserManager<User> userManager)
    {
        _DbContext = dbContext;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Registration(LoginDTO user)
    {
        var exsistingUser = await _userManager.FindByNameAsync(user.Name);
        if (exsistingUser != null)
        {
            return BadRequest(new { message = "Username already exists!" });
        }

        var newUser = new User
        {
            UserName = user.Name
        };

        var result = await _userManager.CreateAsync(newUser, user.Password);

        if (result.Succeeded)
        {
            return Ok(new { message = "Registration success!" });
        }
        else
        {
            // Extract error messages from result.Errors
            var errors = result.Errors.Select(e => e.Description);

            return BadRequest(new 
            { 
                message = "Registration failed.", 
                errors
            });
        }
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDTO user)
    {
        var exsistingUser = await _userManager.FindByNameAsync(user.Name);
        if (exsistingUser != null && await _userManager.CheckPasswordAsync(exsistingUser, user.Password))
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", exsistingUser.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        return Unauthorized(new { message = "Invalid email or password" });
    }
}