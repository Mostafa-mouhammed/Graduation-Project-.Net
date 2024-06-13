using Project.BL.Dtos.Brand;
using Project.BL.Dtos.Product;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;

namespace Project.BL.Mappers.Brandmapper;
public class BrandMapper : IBrandMapper
{
    public BrandReadWithProductDTO brandWithProduct(Brand brand)
    {
        IEnumerable<ProductReadDTO> productsDTO = brand.products?
            .Select(p => new ProductReadDTO(
                p.Id,
                p.Name,
                p.Discription,
                p.Image,
                p.Quantity,
                p.Discount,
                p.rate,
                p.Price,
                p.subCategoryId,
                p.productType.ToString(),
                p.brandId,
                p.variantGroupId
                ));

        return new BrandReadWithProductDTO(brand.Id, brand.Name, productsDTO);
    }

    public async Task<Brand> insertToModel(BrandInsertDTO insert)
    {
        return new Brand()
        {
            Name = insert.Name,
            image = insert.image,
        };
    }

    public BrandAdminDTO modelToAdminRead(Brand brand)
    {
        return new BrandAdminDTO(brand.Id, brand.Name, brand.isDeleted, brand.image);
    }

    public IEnumerable<BrandAdminDTO> modelToAdminReadList(IEnumerable<Brand> brand)
    {
        return brand.Select(b => modelToAdminRead(b));
    }

    public BrandReadDTO modelToRead(Brand brand)
    {
        return new BrandReadDTO(brand.Id, brand.Name, brand.image);
    }

    public IEnumerable<BrandReadDTO> modelToReadList(IEnumerable<Brand> brand)
    {
        return brand.Select(b => modelToRead(b));
    }

    public Brand readToModel(BrandReadDTO brand)
    {
        return new Brand()
        {
            Name = brand.Name,
            image = brand.image,
        };
    }
}
