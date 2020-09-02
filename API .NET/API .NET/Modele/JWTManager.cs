using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API_.NET.Modele
{
    public class JWTManager
    {
        private string SECRET_KEY = "UzJFZk1FRUZVVHlXNE12MWhYVE9td1luejN6U3JqOVAwU3JkdHF3VVNwYVg5WlpVOEZXcXFuckxiVDg1MW5R";

        public JWTManager()
        {
                
        }
        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentException("Given token is null or empty");

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }

        public SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(SECRET_KEY);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
