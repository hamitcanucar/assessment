using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExampleProject.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExampleProject.Helpers
{
    public class GenerateJwtHelper
    {
        private IConfiguration _configuration { get; set; }
        
        public GenerateJwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
 
        public string GenerateJwtToken(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
 
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}