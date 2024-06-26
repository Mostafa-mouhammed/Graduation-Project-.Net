using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Repositories.AttributesRepository;
using Project.DAL.Repositories.Brandrepo;
using Project.DAL.Repositories.CartProduct;
using Project.DAL.Repositories.Carts;
using Project.DAL.Repositories.Categories;
using Project.DAL.Repositories.CategoryImagesRepository;
using Project.DAL.Repositories.EAVProductsRepository;
using Project.DAL.Repositories.OrderItems;
using Project.DAL.Repositories.Orders;
using Project.DAL.Repositories.ProductImageRepository;
using Project.DAL.Repositories.Productsrepository;
using Project.DAL.Repositories.ProductTypeAttributeRepository;
using Project.DAL.Repositories.ProductTypesRepository;
using Project.DAL.Repositories.Ratingrepo;
using Project.DAL.Repositories.SubCategoryImagesRepostiory;
using Project.DAL.Repositories.SubCategoryRepository;
using Project.DAL.Repositories.User;
using Project.DAL.Repositories.ValuesRepository;
using Project.DAL.Repositories.VaraityGroupRepository;
using Project.DAL.Repositories.VariantGroupAttributeRepository;
using Project.DAL.Repositories.WishListrepo;
using Project.DAL.UnitOfWork;

namespace Project.DAL.ExtensionServices;
public static class DALServices
{
    public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAttributeRepository, AttributeRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICartProductRepository, CartProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryImagesRepository, SubCategoryImagesRepository>();
        services.AddScoped<IVariantGroupAttributeRepository, VariantGroupAttributeRepository>();
        services.AddScoped<ICategoryImageRepository, CategoryImageRepository>();
        services.AddScoped<IEAVRepository, EAVRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductImagesRepositiory, ProductImagesRepositiory>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPTARepository, PTARepository>();
        services.AddScoped<IProductTypesRepository, ProductTypesRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<IValuesRepository, ValuesRepository>();
        services.AddScoped<IVaraityGroupRepository, VaraityGroupRepository>();
        services.AddScoped<IWishListRepository, WishListRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

    }
}
