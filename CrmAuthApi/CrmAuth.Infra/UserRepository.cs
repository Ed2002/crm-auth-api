using CrmAuth.Domain.Entities;
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

        public async Task<User> SearchUserByEmail(string Email)
        {
            try
            {
                StringBuilder query = new();

                query.Append(" select id as Id, email as Email, password as Password ");
                query.Append(" from user ");
                query.Append(" where email = @Email; ");

                DynamicParameters parameters = new();

                parameters.Add("Email", Email);

                var obj = await _connection.QueryAsync<User>(query.ToString(), parameters);
                return obj.First();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no banco: " + ex);
            }
            finally
            {
                await _connection.CloseAsync();
            }
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
