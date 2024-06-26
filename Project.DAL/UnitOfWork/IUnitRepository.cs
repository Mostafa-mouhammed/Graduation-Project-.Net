using Microsoft.AspNetCore.Identity;
using Project.DAL.Models;
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

namespace Project.DAL.UnitOfWork;

public interface IUnitRepository
{
    public ICategoryRepository category { get; }
    public ISubCategoryRepository subCategory { get; }
    public IVariantGroupAttributeRepository variantGroupAttribute { get; }
    public IProductRepository product { get; }
    public IProductImagesRepositiory productImages { get; }
    public IProductTypesRepository productTypes { get; }
    public ISubCategoryImagesRepository subCategoryImages { get; }
    public ICategoryImageRepository CategoryImages { get; }
    public IPTARepository productTypeAttribute { get; }
    public ICartProductRepository cartProduct { get; }
    public ICartRepository cart { get; }
    public IEAVRepository EAV { get; }
    public IAttributeRepository attribute { get; }
    public IValuesRepository values { get; }
    public IVaraityGroupRepository varaityGroup { get; }
    public IOrderRepository order { get; }
    public IOrderItemRepository orderItem { get; }
    public IBrandRepository brand { get; }
    public IWishListRepository wishlist { get; }
    public IRatingRepository rating { get; }
    public IUserRepository userReposit { get; }

    public UserManager<User> user { get; }
    Task SaveChanges();
}
