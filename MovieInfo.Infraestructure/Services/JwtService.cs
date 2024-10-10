using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieInfo.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Services;
public class JwtService : IJwtService
{
    public IConfiguration _config;
    public JwtService(IConfiguration config)
    {
        _config = config;   
    }
    public string GenerateToken(List<Claim> claimsForToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimsForToken),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
