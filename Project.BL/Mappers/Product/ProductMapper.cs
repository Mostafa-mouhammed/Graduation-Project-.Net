using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.Brand;
using Project.BL.Dtos.Category;
using Project.BL.Dtos.Product;
using Project.BL.Dtos.ProductImages;
using Project.BL.Mappers.EAVProductsMapper;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.ProductImages;
using Project.DAL.Models;

namespace Project.BL.Mappers.Products;

public class ProductMapper : IProductMapper
{
    private readonly IProductImagesMapper _productImagesMapper;
    private readonly IEAVProductsMapper _EAVMApper;

    public ProductMapper(
        IProductImagesMapper productImagesMapper,
        IEAVProductsMapper EAVProductsMapper
        )
    {
        _productImagesMapper = productImagesMapper;
        _EAVMApper = EAVProductsMapper;

    }
    public async Task<Product> insertSimpleToModel(ProductSimpleInsertDTO insert)
    {
        return new Product()
        {
            Name = insert.Name,
            Discription = insert.description,
            Price = insert.Price,
            subCategoryId = insert.subCategoryId,
            Quantity = insert.Quantity,
            Discount = insert.Discount,
            Image =  insert.Image,
            brandId = insert.brandId,
            productType = ProductType.Simple,
            variantGroupId = null
        };
    }

    public IEnumerable<ProductReadDTO> listModelToReadDTO(IEnumerable<Product> model)
    {
        return model.Select(i => modelToReadDTO(i));
    }

    public ProductAdminReadDTO modelToAdminDTO(Product m)
    {
        return new ProductAdminReadDTO(m.Id, m.Name, m.Discription, m.Image, m.Quantity, m.Discount, m.Price, m.isDeleted, m.rate, m.subCategoryId, m.brandId);
    }

    public IEnumerable<ProductAdminReadDTO> listModelToAdminDTO(IEnumerable<Product> model)
    {
        return model.Select(p => modelToAdminDTO(p));
    }

    public ProductReadDTO modelToReadDTO(Product model)
    {
        return new ProductReadDTO(
            model.Id,
            model.Name,
            model.Discription,
            model.Image,
            model.Quantity,
            model.Discount,
            model.rate,
            model.Price,
            model.subCategoryId,
            model.productType.ToString(),
            model.brandId,
            model.variantGroupId
            );
    }

    public ProductOneDTO modelToOneOnlyRead(Product model,IEnumerable<EAVProducts> eavProducts)
    {
        CategoryReadDTO categoryDTO = new CategoryReadDTO(model.subCategory.Id, model.subCategory.Name, model.Discription, model.Image);
        BrandReadDTO brandDTO = new BrandReadDTO(model.Brand.Id, model.Brand.Name, model.Brand.image);
        IEnumerable<ProductImagesReadDTO>? productImagesDTO = _productImagesMapper.modelToReadList(model.Images ?? []);
        IEnumerable<AttributeWithValuesReadDTO> versions = _EAVMApper.EavOneProductList(eavProducts);


        return new ProductOneDTO(
            model.Id,
            model.Name,
            model.Discription,
            model.Image,
            model.Discount,
            model.rate,
            model.Quantity,
            model.Price,
            model.isDeleted,
            model.productType,
            model.variantGroupId,
            brandDTO,
            categoryDTO,
            productImagesDTO,
            versions
            );
    }

    public ProductAdminPaginationDTO toAdminPagination(IEnumerable<ProductAdminReadDTO> products, int total)
    {
        return new ProductAdminPaginationDTO(products, total);
    }

    public ProductGeneralPaginationDTO toGeneralPagination(IEnumerable<ProductReadDTO> products, int total)
    {
        return new ProductGeneralPaginationDTO(products, total);
    }

    public async Task<Product> insertVarToModel(ProductVarInsertDTO insert)
    {
        return new Product()
        {
            Name = insert.Name,
            Discription = insert.description,
            Discount = insert.Discount,
            brandId = insert.brandId,
            subCategoryId = insert.subCategoryId,
            isDeleted = false,
            Price = insert.Price,
            Quantity = insert.Quantity,
            variantGroupId = insert.varGroupId,
            productType = ProductType.variety,
            Image = insert.Image != null ? insert.Image:"",
        };
    }
}
