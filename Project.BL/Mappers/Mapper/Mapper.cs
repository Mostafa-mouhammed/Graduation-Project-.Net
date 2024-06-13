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

public class Mapper : IMapper
{
    public Mapper(ICartProductMapper cartProductMapper,
        ICartMapper cartMapper,
        IImageMapper imageMapper,
        IEAVProductsMapper EAVMapper,
        ICategoryMapper categoryMapper,
        IOrderMapper orderMapper,
        IProductImagesMapper productImagesmapper,
        IOrderItemMapper orderItemMapper,
        IProductMapper productMapper,
        IPTAMapper PTAMapper,
        ISubCategoryImageMapper subCategoryImageMapper,
        IBrandMapper brandMapper,
        ICategoryImageMapper categoryImageMapper,
        IRatingMapper ratingMapper,
        IAttributeMapper attributeMapper,
        IVariantGroupAttributeMapper variantGroupAttributeMapper,
        IValuesMapper valuesMapper,
        IProductTypeMapper productTypeMapper,
        ISubCategoryMapper subCategoryMapper,
        IVariantGroupMapper variantGroupMapper,
        IWishListMapper wishListMapper,
        IUserMapper userMapper)
    {
        cartProduct = cartProductMapper;
        cart = cartMapper;
        VariantGroupAttribute = variantGroupAttributeMapper;
        subCategoryImage = subCategoryImageMapper;
        categoryImages = categoryImageMapper;
        brand = brandMapper;
        subCategory = subCategoryMapper;
        image = imageMapper;
        rating = ratingMapper;
        productType = productTypeMapper;
        PTA = PTAMapper;
        attribute = attributeMapper;
        variantGroup = variantGroupMapper;
        values = valuesMapper;
        productImages = productImagesmapper;
        EAV = EAVMapper;
        wishList = wishListMapper;
        category = categoryMapper;
        order = orderMapper;
        orderItem = orderItemMapper;
        product = productMapper;
        user = userMapper;
    }
    public ICartProductMapper cartProduct { get; } = null!;
    public ICartMapper cart { get; } = null!;
    public ICategoryImageMapper categoryImages { get; }
    public ISubCategoryImageMapper subCategoryImage { get; }
    public IVariantGroupAttributeMapper VariantGroupAttribute { get; }
    public IProductImagesMapper productImages { get; }
    public ISubCategoryMapper subCategory { get; }
    public IProductTypeMapper productType { get; }
    public IImageMapper image { get; } = null!;
    public IPTAMapper PTA { get; }
    public IAttributeMapper attribute { get; } = null!;
    public IVariantGroupMapper variantGroup { get; } = null!;
    public IValuesMapper values { get; } = null!;
    public IEAVProductsMapper EAV { get; } = null!;
    public ICategoryMapper category { get; } = null!;
    public IOrderMapper order { get; } = null!;
    public IOrderItemMapper orderItem { get; } = null!;
    public IProductMapper product { get; } = null!;
    public IBrandMapper brand { get; } = null!;
    public IRatingMapper rating { get; } = null!;
    public IWishListMapper wishList { get; } = null!;
    public IUserMapper user { get; } = null!;
}