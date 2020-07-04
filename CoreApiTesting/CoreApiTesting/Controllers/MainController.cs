using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoreApiTesting.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpGet("gettoken")]
        public Object GetToken()
        {
            string key = "my_secret_key_12345";
            var issuer = "http://mysite.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            var token = new JwtSecurityToken(issuer,
                            issuer,
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };


        }












        [HttpGet("getname")]
        public String GetName()
        {
            return "Bilal Shahzad";
        }

        [HttpGet("getmarks")]
        public int Getmarks(int rollnumber)
        {
             return 100;
        }

        [HttpPost("saveuser")]
        public String Save(StudentDTO dto)
        {
            return "Done";
        }
    }

    public class StudentDTO
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
    }

}
