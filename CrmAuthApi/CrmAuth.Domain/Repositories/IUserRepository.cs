using CrmAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmAuth.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> SearchUserByEmail(string Email);
        Task<List<long>> UserIdsList();
    }
}
