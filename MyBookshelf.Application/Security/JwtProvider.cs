using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyBookshelf.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBookshelf.Application.Security
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email)
        {
            var issuer = _configuration["MyBookshelf:JwtTokenIssuer"];
            var audience = _configuration["MyBookshelf:JwtTokenAudience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["MyBookshelf:JwtTokenSecret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("userName", email)
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials,
                claims: claims
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
