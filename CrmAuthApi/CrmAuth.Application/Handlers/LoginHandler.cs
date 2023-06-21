using CrmAuth.Application.Commands;
using CrmAuth.Domain.Entities;
using CrmAuth.Domain.Model;
using CrmAuth.Domain.Repositories;
using CrmAuth.Infra;
using CrmAuth.Utils;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CrmAuth.Application.Handlers
{
    public class LoginHandler
    {
        private IUserRepository userRepository;
        private Util util;
        private string token;

        public LoginHandler(MySqlConnection connection, string _token)
        {
            userRepository = new UserRepository(connection);
            util = new();
            token = _token;
        }

        public ResultModel<string> Handle(LoginCommand request)
        {
            try
            {
                ResultModel<string> result = new();

                var user = userRepository.SearchUserByEmail(request.Email).Result;

                if (!util.VerifyPasswordHash(request.Password, Convert.FromBase64String(user.Password), Convert.FromBase64String(user.PasswordSalt)))
                {
                    result.Success = false;
                    result.Model = "Senha Invalida!";
                    return result;

                }

                string Token = CreateToken(user);

                result.Success = true;
                result.Model = Token;

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("" + ex);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("name", user.Nome),
                new Claim("email", user.Email)
            };

            // Geração da chave secreta
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var payload = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(40),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenString = tokenHandler.WriteToken(payload);

            return tokenString;
        }
    }
}
