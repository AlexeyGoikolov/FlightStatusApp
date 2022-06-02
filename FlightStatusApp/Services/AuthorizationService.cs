using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FlightStatusApp.Config;
using FlightStatusApp.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace FlightStatusApp.Services;

public class AuthorizationService
{
    private IUserRepository _db;
    private IConfiguration _configuration;

    public AuthorizationService(IUserRepository db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public async Task<dynamic> GetToken(string username, string password)
    {
        var identity = await GetIdentity(username, password);
        if (identity is null)
            return new ArgumentException("Некорректные логин или пароль");
        var now = DateTime.UtcNow;
            
        var jwt = new JwtSecurityToken(
            issuer: _configuration.GetSection("token:issuer" ).Value,
            audience: _configuration.GetSection("token:audience" ).Value,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(Convert.ToDouble(_configuration.GetSection("token:lifeTime" ).Value))),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_configuration.GetSection("token:secret" ).Value),
                SecurityAlgorithms.HmacSha256)
        );
            
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };

        return response;
    }
    

    private async Task<ClaimsIdentity> GetIdentity(string username, string password)
    {
        var user = await _db.GetUser(username);
        if (user != null && PasswordHashGenerator.IsValidPassword(user.Password, password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Code)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        return null;
    }
}