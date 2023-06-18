using CrmAuth.Application.Commands;
using CrmAuth.Domain.Entities;
using CrmAuth.Domain.Repositories;
using CrmAuth.Infra;
using CrmAuth.Utils;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CrmAuth.Application.Handlers
{
    public class LoginHandler
    {
        private IUserRepository userRepository;
        private Util util;

        public LoginHandler(MySqlConnection connection)
        {
            userRepository = new UserRepository(connection);
            util = new();
        }

        public string Handle(LoginCommand request)
        {
            try
            {
                var user = userRepository.SearchUserByEmail(request.Email).Result;

                if (!util.VerifyPasswordHash(request.Password, Convert.FromBase64String(user.Password), Convert.FromBase64String(user.PasswordSalt)))
                {
                    return "User Invalid";
                }

                string Token = CreateToken(user);

                return Token;
            }
            catch(Exception ex)
            {
                throw new Exception("" + ex);
            }
        }

        private string CreateToken(User user)
        {
            byte[] keyBytes = new byte[64];

            var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("nome", user.Nome),
                new Claim("email", user.Email),
            };

            var key = new SymmetricSecurityKey(keyBytes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var MYtoken = tokenHandler.CreateToken(tokenDescriptor);

            string tokenString = tokenHandler.WriteToken(MYtoken);

            return tokenString;
        }
    }
}
