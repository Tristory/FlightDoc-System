using FlightDocs.Data;
using FlightDocs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightDocs.Controllers
{
    public class IdentityController : Controller
    {
        private static string key = "The Super Secret";
        public AccountDM accountDM;

        public IdentityController(ApplicationDbContext context) 
        {
            accountDM = new AccountDM(context);
        }

        [HttpPost]
        [Route("Token Generator")]
        public IActionResult TokenGenerator(Account account)
        {
            List<Claim> claims;

            if (account != null)
            {
                claims = new List<Claim>()
                {
                    new Claim("Id", account.Id.ToString()),
                    new Claim("Name", account.User.Name),
                    new Claim("Role", account.Role.Name)
                };
            }
            else
            {
                claims = new List<Claim>()
                {
                    new Claim("Id", "000"),
                    new Claim("Name", "Zawarudo"),
                    new Claim("Role", "admin")
                };
            }

            

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

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(string name, string password)
        {
            if (name == null || password == null)
            {
                return Content("Please input user correctly!");
            }

            User user = new User();
            user.Name = name;
            user.Password = password;

            accountDM.AddUser(user);

            return Ok();
        }

        [HttpPost]
        [Route("Generate Account")]
        public IActionResult GenerateAccount(AccountInfo accountInfo)
        {
            if (accountInfo == null)
                return Content("Please input account");

            var account = new Account();
            account.IsActive = accountInfo.IsActive;
            account.UserId = accountInfo.UserId;
            account.RoleId = accountInfo.RoleId;
            account.GroupId = accountInfo.GroupId;

            accountDM.AddAccount(account);

            return Ok();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string name, string password)
        {
            Account account = accountDM.GetUserAccount(name, password);

            if (account == null)
                return Content("Opp! Something is wrong");

            return TokenGenerator(account);
        }

        /*public static string CurrentUserId(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenString);
            var userId = token.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value;

            return userId;
        }

        public static bool UserIsAdmin(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenString);
            var userRole = token.Claims.FirstOrDefault(cl => cl.Type == "Role")?.Value;

            if(userRole != "admin")
                return false;

            return true;
        }

        public async Task<string> GetCurrentUserTokenAsync()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            if (token == null)
            {
                return "Is null";
            }

            return token;
        }

        //For test only
        public async Task<ActionResult> EditLibrary()
        {
            string token = await GetCurrentUserTokenAsync();

            bool IsAdmin = UserIsAdmin(token);

            return Ok(token);
        }*/
    }
}
