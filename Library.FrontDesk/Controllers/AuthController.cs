using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.FrontDesk.Controllers
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Permissions
    {
        public bool Write { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

        public Permissions(bool allowAl)
        {
            if (allowAl)
            {
                Write = true;
                Read = true;
                Update = true;
                Delete = true;
            }
        }
    }
    
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request; please provide username and password");
            }

            if (user.Username == "jaysee" && user.Password == "pass321")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("JwtToken").GetValue<string>("Base64Secret")
                ));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration.GetSection("JwtToken").GetValue<string>("Issuer"),
                    audience: _configuration.GetSection("JwtToken").GetValue<string>("Audience"),
                    claims: new List<Claim>
                    {
                        new Claim("userId", "1"),
                        new Claim("role", "admin")
                        // new Claim("permissions", JsonSerializer.Serialize(new Permissions(true)))
                    },
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                );
            
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateIdentity()
        {
            return Ok("you're good to go");
        }
    }
}