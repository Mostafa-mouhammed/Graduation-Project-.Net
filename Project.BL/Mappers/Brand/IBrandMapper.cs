using Project.BL.Dtos.Brand;
using Project.DAL.Models;

namespace Project.BL.Mappers.Brandmapper;
public interface IBrandMapper
{
    BrandReadDTO modelToRead(Brand brand);
    Brand readToModel(BrandReadDTO brand);
    Task<Brand> insertToModel(BrandInsertDTO insert);
    IEnumerable<BrandReadDTO> modelToReadList(IEnumerable<Brand> brand);
    BrandReadWithProductDTO brandWithProduct(Brand brand);
    BrandAdminDTO modelToAdminRead(Brand brand);
    IEnumerable<BrandAdminDTO> modelToAdminReadList(IEnumerable<Brand> brand);

}