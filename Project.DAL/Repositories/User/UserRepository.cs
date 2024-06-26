using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;
using Project.DAL.Repositories.Ratingrepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.User
{
    public class UserRepository : GenericRepository<Models.User>, IUserRepository
    {
        public UserRepository(APIContext context) : base(context)
        {
        }
        public async Task<Models.User>? GetUser(string id)
        {
            return await _context.Set<Models.User>().FindAsync(id);
        }

    }
}
