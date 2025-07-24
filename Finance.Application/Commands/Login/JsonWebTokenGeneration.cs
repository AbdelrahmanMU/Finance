using Finance.Application.DTOs;
using Finance.Domain;
using Finance.Shared.CurrentUser;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finance.Application.Commands.Login;

public static class JsonWebTokenGeneration
{
    public static string GenerateJwtToken(User user, string? role, JwtConfig jwtConfig)
    {
        var signingKey = Encoding.UTF8.GetBytes(jwtConfig.Key);

        #region Add Claims

        var claims = new List<Claim>();

        claims.AddClaim(ClaimKeys.Id, user.Id.ToString());
        claims.AddClaim(ClaimKeys.Email, user.Email);
        claims.AddClaim(ClaimKeys.FirstName, user.FirstName);
        claims.AddClaim(ClaimKeys.LastName, user.LastName);

        claims.AddImageClaim(user);

        claims.AddClaim(ClaimKeys.Role, role);

        #endregion

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now,
            Expires = DateTime.Now.AddMinutes(jwtConfig.ExpiryDurationInMinutes),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey),
                SecurityAlgorithms.HmacSha256)
        };

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var token = jwtTokenHandler.WriteToken(jwtToken);

        return token;
    }

    private static void AddClaim(this ICollection<Claim> claims, string propKey, string? propValue)
    {
        if (propValue is null) return;
        claims.Add(new Claim(propKey, propValue));
    }

    private static void AddImageClaim(this ICollection<Claim> claims, User user)
    {
        var imageClaim = user.Image;
        if (imageClaim is null) return;
        claims.AddClaim(ClaimKeys.ImageUrl, imageClaim.Url);
    }

}
