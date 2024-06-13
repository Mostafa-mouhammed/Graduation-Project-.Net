using Microsoft.Extensions.DependencyInjection;
using Project.BL.Services.AttributeService;
using Project.BL.Services.AuthService;
using Project.BL.Services.BrandService;
using Project.BL.Services.CartProductService;
using Project.BL.Services.CartService;
using Project.BL.Services.CategoryService;
using Project.BL.Services.EAVService;
using Project.BL.Services.Image;
using Project.BL.Services.OrderItemService;
using Project.BL.Services.OrderService;
using Project.BL.Services.ProductService;
using Project.BL.Services.ProductTypeAttributeService;
using Project.BL.Services.ProductTypes;
using Project.BL.Services.RatingService;
using Project.BL.Services.SubCategoryService;
using Project.BL.Services.UnitService;
using Project.BL.Services.UserService;
using Project.BL.Services.ValuesService;
using Project.BL.Services.VaraityGroup;
using Project.BL.Services.WishListService;

namespace Project.DAL.ExtensionServices;
public static class BLServices
{
    public static void AddBLServices(this IServiceCollection services)
    {
        services.AddScoped<IAttributeService, AttributeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<ICartProductService, CartProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderItemService, OrderItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IEAVService, EAVService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductTypesService, ProductTypesService>();
        services.AddScoped<IPTAService, PTAService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVaraityGroupService, VaraityGroupService>();
        services.AddScoped<IValueService, ValuesService>();
        services.AddScoped<IWishListService, WishListService>();
    }
}
