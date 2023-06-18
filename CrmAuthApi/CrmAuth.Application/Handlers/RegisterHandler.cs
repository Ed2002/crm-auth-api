using CrmAuth.Application.Commands;
using CrmAuth.Domain.Repositories;
using CrmAuth.Domain.Util;
using CrmAuth.Infra;
using CrmAuth.Utils;
using MySql.Data.MySqlClient;

namespace CrmAuth.Application.Handlers
{
    public class RegisterHandler
    {
        private IUserRepository userRepository;
        private Util util;

        public RegisterHandler(MySqlConnection connection)
        {
            userRepository = new UserRepository(connection);
            util = new();
        }

        public long Handle(RegisterCommand request)
        {
            try
            {
                if(userRepository.VerifyUserEmail(request.Email).Result != 0)
                {
                    throw new Exception("Email sendo usado");
                }

                Password p = util.CreatePasswordHash(request.Password);

                long IdRegister = userRepository.RegisterUser(request.Email, Convert.ToBase64String(p.PasswordHash), Convert.ToBase64String(p.PasswordSalt)).Result;

                return IdRegister;
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex);
            }
        }
    }
}
