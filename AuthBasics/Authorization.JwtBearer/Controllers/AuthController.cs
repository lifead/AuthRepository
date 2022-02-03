using Authorization.JwtBearer.ConfigurationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.JwtBearer.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateToken()
        {
            List<Claim>? claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , "Admin"),
                new Claim(JwtRegisteredClaimNames.Email, "admin@example.com"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "user")
            };

            byte[] key_bytes = Encoding.UTF8.GetBytes(JwtConfiguration.SecretKey);
            SymmetricSecurityKey key = new(key_bytes);
            SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                JwtConfiguration.Issuer,
                JwtConfiguration.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            ViewBag.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return View();
        }
    }
}
