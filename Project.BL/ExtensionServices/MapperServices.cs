using Microsoft.Extensions.DependencyInjection;
using Project.BL.Mappers.AttributesMapper;
using Project.BL.Mappers.Brandmapper;
using Project.BL.Mappers.CartProductsm;
using Project.BL.Mappers.Carts;
using Project.BL.Mappers.Categories;
using Project.BL.Mappers.CategoryImages;
using Project.BL.Mappers.EAVProductsMapper;
using Project.BL.Mappers.Images;
using Project.BL.Mappers.Mapper;
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

namespace Project.BL.ExtensionServices;
public static class MapperServices
{
    public static void AddMapperServices(this IServiceCollection services)
    {
        services.AddScoped<IAttributeMapper, AttributeMapper>();
        services.AddScoped<IBrandMapper, BrandMapper>();
        services.AddScoped<ICartProductMapper, CartProductMapper>();
        services.AddScoped<ICartMapper, CartMapper>();
        services.AddScoped<ISubCategoryMapper, SubCategoryMapper>();
        services.AddScoped<ISubCategoryImageMapper, SubCategoryImageMapper>();
        services.AddScoped<IVariantGroupAttributeMapper, VariantGroupAttributeMapper>();
        services.AddScoped<ICategoryImageMapper, CategoryImageMapper>();
        services.AddScoped<ICategoryMapper, CategoryMapper>();
        services.AddScoped<ICategoryImageMapper, CategoryImageMapper>();
        services.AddScoped<IEAVProductsMapper, EAVProductsMapper>();
        services.AddScoped<IImageMapper, ImageMapper>();
        services.AddScoped<IMapper, Mapper>();
        services.AddScoped<IOrderMapper, OrderMapper>();
        services.AddScoped<IOrderItemMapper, OrderItemMapper>();
        services.AddScoped<IProductMapper, ProductMapper>();
        services.AddScoped<IProductImagesMapper, ProductImagesMapper>();
        services.AddScoped<IProductTypeMapper, ProductTypeMapper>();
        services.AddScoped<IPTAMapper, PTAMapper>();
        services.AddScoped<IRatingMapper, RatingMapper>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IValuesMapper, ValuesMapper>();
        services.AddScoped<IVariantGroupMapper, VariantGroupMapper>();
        services.AddScoped<IWishListMapper, WishListMapper>();
    }
}
