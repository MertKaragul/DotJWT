using DotJwt.Interface;
using DotJwt.Model;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DotJwt.Service.Jwt {
    public class JwtService {

        public async Task<String> GenerateToken(IConfiguration configuration,UserModel? userModel)
        {
            if(userModel == null) throw new ArgumentNullException(nameof(userModel));

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials =  new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Name),
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim(ClaimTypes.Role, userModel.Roles)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["Token:Issuer"],
                audience: configuration["Token:Audience"],
                signingCredentials: signingCredentials,
                notBefore: DateTime.Now,
                claims: claims,
                expires: DateTime.Now.AddSeconds(Double.Parse(configuration["Token:Expiration"])
                ));
     
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Boolean> VerifyToken(IConfiguration configuration,string? token)
        {
            if(token == null) throw new ArgumentNullException("Token is required");
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            try
            {
                jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters()
                 {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidAudience = configuration["Token:Audience"],
                    IssuerSigningKey = securityKey,
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
