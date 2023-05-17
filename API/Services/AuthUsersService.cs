using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APITrassBank.Services
{
    public interface IAuthUsersService
    {
        Task<string> GenerateJWT(IdentityUser user);
    }
    public class AuthUsersService : IAuthUsersService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string jwt_Key;
        private readonly string jwt_Issuer;
        private readonly string jwt_Audience;

        public AuthUsersService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            jwt_Issuer = configuration.GetSection("Jwt")["Issuer"];
            jwt_Key = configuration.GetSection("Jwt")["Key"];
            jwt_Audience = configuration.GetSection("Jwt")["Audience"];
        }
        public async Task<string> GenerateJWT(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            // Generamos un token según los claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt_Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwt_Issuer,
                audience: jwt_Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
