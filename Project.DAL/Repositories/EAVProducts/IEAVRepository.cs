using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.EAVProductsRepository;
public interface IEAVRepository : IGenericRepository<EAVProducts>
{
    Task<IEnumerable<EAVProducts>> getEAVbyGroupId(int groupId);
    Task<IEnumerable<EAVProducts>> getEavbyGroupOnly(int groupId);
}
