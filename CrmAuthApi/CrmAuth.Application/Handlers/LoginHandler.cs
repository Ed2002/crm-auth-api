using CrmAuth.Domain.Repositories;
using CrmAuth.Infra;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmAuth.Application.Handlers
{
    public class LoginHandler
    {
        private IUserRepository userRepository;

        public LoginHandler(MySqlConnection connection)
        {
            userRepository = new UserRepository(connection);
        }

        public bool Handle(string Email)
        {
            try
            {
                var user = userRepository.SearchUserByEmail(Email).Result;

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("" + ex);
            }
        }
    }
}
