using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.AttributesRepository;
public interface IAttributeRepository : IGenericRepository<Attributes>
{
   Task<Attributes> getAttrbuitebyName(string attributes);
    Task<IEnumerable<int>> checkAllAttributesExiest(IEnumerable<int> attributesIds);
}
