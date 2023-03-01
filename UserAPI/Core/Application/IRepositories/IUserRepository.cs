using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Core.Domain.Model;

namespace UserAPI.Core.Application.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        
    }
}
