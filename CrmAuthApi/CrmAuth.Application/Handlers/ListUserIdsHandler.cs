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
    public class ListUserIdsHandler
    {

        private IUserRepository userRepository;

        public ListUserIdsHandler(MySqlConnection connection) 
        {
            userRepository = new UserRepository(connection);
        }

        public List<long> Handle()
        {
            try
            {
                return userRepository.UserIdsList().Result;
            }
            catch(Exception ex)
            {
                throw new Exception(""+ex);
            }
        }
    }
}
