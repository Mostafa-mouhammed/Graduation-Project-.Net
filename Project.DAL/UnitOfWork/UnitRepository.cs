using Microsoft.AspNetCore.Identity;
using Project.DAL.Data;
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
using Project.DAL.Repositories.ValuesRepository;
using Project.DAL.Repositories.VaraityGroupRepository;
using Project.DAL.Repositories.VariantGroupAttributeRepository;
using Project.DAL.Repositories.WishListrepo;
using Project.DAL.Repositories.User; 

namespace Project.DAL.UnitOfWork
{
    public class UnitRepository : IUnitRepository
    {
        public UnitRepository(
            IAttributeRepository attributeRepository,
            IBrandRepository brandRepository,
            ICartProductRepository cartProductRepository,
            ICartRepository cartRepository,
            ICategoryRepository categoryRepository,
            IEAVRepository EAVRepository,
            IOrderItemRepository orderItemRepository,
            IProductTypesRepository productTypesRepository,
            IOrderRepository orderRepository,
            IPTARepository productTypeAttributeRepository,
            IProductImagesRepositiory productImagesRepositor,
            IVariantGroupAttributeRepository variantGroupAttributeRepository,
            IProductRepository productRepository,
            ISubCategoryImagesRepository subCategoryImagesRepository,
            IRatingRepository ratingRepository,
            ISubCategoryRepository subCategoryRepository,
            IValuesRepository valuesRepository,
            ICategoryImageRepository categoryImageRepository,
            IVaraityGroupRepository varaityGroupRepository,
            IWishListRepository wishListRepository,
            UserManager<User> userManger,
            IUserRepository userRepository, 
            APIContext context)
        {
            category = categoryRepository;
            subCategory = subCategoryRepository;
            CategoryImages = categoryImageRepository;
            product = productRepository;
            variantGroupAttribute = variantGroupAttributeRepository;
            subCategoryImages = subCategoryImagesRepository;
            productTypes = productTypesRepository;
            EAV = EAVRepository;
            productTypeAttribute = productTypeAttributeRepository;
            varaityGroup = varaityGroupRepository;
            productImages = productImagesRepositor;
            cartProduct = cartProductRepository;
            order = orderRepository;
            cart = cartRepository;
            attribute = attributeRepository;
            values = valuesRepository;
            rating = ratingRepository;
            brand = brandRepository;
            wishlist = wishListRepository;
            orderItem = orderItemRepository;
            user = userManger;
            userReposit = userRepository; 
            _context = context;
        }

        public ICategoryRepository category { get; } = null!;
        public IEAVRepository EAV { get; } = null!;
        public IPTARepository productTypeAttribute { get; }
        public IVariantGroupAttributeRepository variantGroupAttribute { get; }
        public ISubCategoryImagesRepository subCategoryImages { get; }
        public IAttributeRepository attribute { get; } = null!;
        public IValuesRepository values { get; } = null!;
        public ISubCategoryRepository subCategory { get; } = null!;
        public IProductRepository product { get; } = null!;
        public IProductTypesRepository productTypes { get; } = null!;
        public IVaraityGroupRepository varaityGroup { get; } = null!;
        public IProductImagesRepositiory productImages { get; } = null!;
        public ICartProductRepository cartProduct { get; } = null!;
        public ICartRepository cart { get; } = null!;
        public IOrderRepository order { get; } = null!;
        public IOrderItemRepository orderItem { get; } = null!;
        public IBrandRepository brand { get; } = null!;
        public IWishListRepository wishlist { get; } = null!;
        public ICategoryImageRepository CategoryImages { get; }
        public IRatingRepository rating { get; } = null!;
        public UserManager<User> user { get; } = null!;
        public IUserRepository userReposit { get; } = null!;


        private readonly APIContext _context;

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


    }
}
