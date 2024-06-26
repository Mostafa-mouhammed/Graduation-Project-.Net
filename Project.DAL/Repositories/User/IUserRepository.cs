using Project.DAL.Models;
using Project.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.User
{
    public interface IUserRepository : IGenericRepository<Models.User>
    {
        Task<Models.User>? GetUser(string id);
    }
}
