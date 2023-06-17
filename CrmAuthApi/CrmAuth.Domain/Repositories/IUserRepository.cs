using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmAuth.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<long>> UserIdsList();
    }
}
