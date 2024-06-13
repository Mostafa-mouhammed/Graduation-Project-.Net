using Project.BL.Mappers.AttributesMapper;
using Project.BL.Mappers.Brandmapper;
using Project.BL.Mappers.CartProductsm;
using Project.BL.Mappers.Carts;
using Project.BL.Mappers.Categories;
using Project.BL.Mappers.CategoryImages;
using Project.BL.Mappers.EAVProductsMapper;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.OrderItems;
using Project.BL.Mappers.Orders;
using Project.BL.Mappers.ProductImages;
using Project.BL.Mappers.Products;
using Project.BL.Mappers.ProductTypes;
using Project.BL.Mappers.PTA;
using Project.BL.Mappers.Ratingmapper;
using Project.BL.Mappers.SubCategoryImagesMapper;
using Project.BL.Mappers.SubCategoryMapper;
using Project.BL.Mappers.Users;
using Project.BL.Mappers.ValuesMapper;
using Project.BL.Mappers.VariantGroupAttributeMapper;
using Project.BL.Mappers.VariantGroupMapper;
using Project.BL.Mappers.WishListmapper;

namespace Project.BL.Mappers.Mapper;

public interface IMapper
{
    public ICartProductMapper cartProduct { get; }
    public ICartMapper cart { get; }
    public ICategoryImageMapper categoryImages { get; }
    public ISubCategoryImageMapper subCategoryImage { get; }
    public IVariantGroupAttributeMapper VariantGroupAttribute { get; }
    public IProductImagesMapper productImages { get; }
    public IImageMapper image { get; }
    public ISubCategoryMapper subCategory { get; }
    public IEAVProductsMapper EAV { get; }
    public IProductTypeMapper productType { get; }
    public IPTAMapper PTA { get; }
    public IAttributeMapper attribute { get; }
    public IVariantGroupMapper variantGroup { get; }
    public IValuesMapper values { get; }
    public ICategoryMapper category { get; }
    public IOrderMapper order { get; }
    public IOrderItemMapper orderItem { get; }
    public IProductMapper product { get; }
    public IBrandMapper brand { get; }
    public IRatingMapper rating { get; }
    public IWishListMapper wishList { get; }
    public IUserMapper user { get; }
}
