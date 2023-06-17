using CrmAuth.Domain.Repositories;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmAuth.Infra
{
    public class UserRepository : IUserRepository
    {
        private MySqlConnection _connection;

        public UserRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<long>> UserIdsList()
        {
            try
            {
                StringBuilder query = new();

                query.Append(" select id ");
                query.Append(" from user; ");

                var obj = await _connection.QueryAsync<long>(query.ToString());
                return obj.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no banco: "+ex);
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
