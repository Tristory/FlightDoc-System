using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightDocs.Controllers
{
    public class IdentityController : Controller
    {
        private static string key = "The Super Secret";

        [HttpPost]
        [Route("Token Generator")]
        public IActionResult TokenGenerator()
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", "100"),
                new Claim("Name", "Zawarudo")
            };

            var identity = new ClaimsIdentity(claims, "MyAuthType");
            var principal = new ClaimsPrincipal(identity);

            var tokenHanlder = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = principal.Identity as ClaimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature  )
            };

            var token = tokenHanlder.CreateToken(tokenDescription);
            var tokenString = tokenHanlder.WriteToken(token);

            return Ok(new {Token = tokenString});
        }
    }
}
