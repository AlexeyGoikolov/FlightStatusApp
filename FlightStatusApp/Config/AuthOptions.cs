using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FlightStatusApp.Config;

public static class AuthOptions
{
    public static void AddJwtConfiguration(this WebApplicationBuilder builder)
    {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration.GetSection("token:issuer" ).Value,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration.GetSection("token:audience" ).Value,
                    ValidateLifetime = true,
                    IssuerSigningKey = GetSymmetricSecurityKey(builder.Configuration.GetSection("token:key" ).Value),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
    }

    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}