using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.VariantGroupAttributeRepository;
public interface IVariantGroupAttributeRepository : IGenericRepository<VariantGroupAttributes>
{
    Task<IEnumerable<VariantGroupAttributes>> getGroupAttributeValues(int Id);
}
