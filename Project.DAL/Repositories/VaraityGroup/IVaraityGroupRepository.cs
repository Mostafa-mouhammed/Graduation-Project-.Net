using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.VaraityGroupRepository;
public interface IVaraityGroupRepository: IGenericRepository<VariantGroup>
{
    Task<VariantGroup?>? getGroupbyName(string name);
    Task<VariantGroup?>? getGroupWithAttributes(int Id);
}
