using CrmAuth.Domain.Entities;
using CrmAuth.Domain.Repositories;
using Dapper;
using MySql.Data.MySqlClient;
using System.Text;

namespace CrmAuth.Infra
{
    public class UserRepository : IUserRepository
    {
        private MySqlConnection _connection;

        public UserRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<long> RegisterUser(string Email, string Passoword, string PasswordSalt)
        {
            try
            {
                StringBuilder query = new();

                query.Append(" insert into user (email,password,password_salt) ");
                query.Append(" values(@email,@password,@password_salt); ");
                query.Append(" SELECT LAST_INSERT_ID(); ");

                DynamicParameters parameters = new();

                parameters.Add("email", Email);
                parameters.Add("password", Passoword);
                parameters.Add("password_salt", PasswordSalt);

                var obj = await _connection.QueryAsync<long>(query.ToString(),parameters);
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

        public async Task<long> VerifyUserEmail(string Email)
        {
            try
            {
                StringBuilder query = new();

                query.Append(" select id ");
                query.Append(" from user ");
                query.Append(" where email = @Email; ");

                DynamicParameters parameters = new();

                parameters.Add("Email", Email);

                var obj = await _connection.QueryAsync<long>(query.ToString(), parameters);
                return obj.FirstOrDefault();
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
        public async Task<User> SearchUserByEmail(string Email)
        {
            try
            {
                StringBuilder query = new();

                query.Append(" select id as Id, name as Nome, email as Email, password as Password, password_salt as PasswordSalt ");
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
    }
}
