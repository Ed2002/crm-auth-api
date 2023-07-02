using CrmAuth.Domain.Entities;

namespace CrmAuth.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<long> RegisterUser(string Name, string Email, string Passoword, string PasswordSalt);
        Task<long> VerifyUserEmail(string Email);
        Task<User> SearchUserByEmail(string Email);
    }
}
