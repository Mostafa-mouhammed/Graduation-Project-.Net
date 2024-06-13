using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ProductTypeAttributeRepository;
public interface IPTARepository : IGenericRepository<PTA>
{
    Task<IEnumerable<Attributes>> getAllAtrributesbyType(int productType);
    Task<PTA> getPTAbyattrIdProudctType(int ProductTypeId, int AttribuiteId);
}
