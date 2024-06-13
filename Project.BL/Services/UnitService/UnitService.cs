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
using Project.BL.Services.UserService;
using Project.BL.Services.ValuesService;
using Project.BL.Services.VaraityGroup;
using Project.BL.Services.WishListService;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.UnitService;
public class UnitService : IUnitService
{
    public UnitService(
        ICartProductService cartProductService,
        ICartService cartService,
        ICategoryService categoryService,
        IOrderService orderService,
        IProductService productService,
        IOrderItemService orderItemService,
        IAuthService authService,
        IAttributeService attributeService,
        IImageService imageservice,

        IPTAService PTAService,
        IValueService valuesService,
        IUserService userService,
        IBrandService brandService,
        IWishListService wishListService,
        IProductTypesService productTypesService,
        IEAVService EAVService,
        ISubCategoryService subCategoryservice,
        IVaraityGroupService variantGroupService,
        IRatingService ratingService,
        IUnitRepository unitRepository
        )
    {
        cartproduct = cartProductService;
        cart = cartService;
        productTypes = productTypesService;
        values = valuesService;
        subCategory = subCategoryservice;
        variantGroup = variantGroupService;
        EAV = EAVService;
        image = imageservice;
        categories = categoryService;
        order = orderService;
        product = productService;
        attribute = attributeService;
        PTA = PTAService;
        orderitem = orderItemService;
        auth = authService;
        user = userService;
        brand = brandService;
        wishList = wishListService;
        rating = ratingService;
        _unitrepository = unitRepository;
    }
    public ICartProductService cartproduct { get; set; } = null!;
    public IProductTypesService productTypes { get; }
    public IImageService image { get; }
    public ICartService cart { get; } = null!;
    public ICategoryService categories { get; } = null!;
    public IOrderService order { get; } = null!;
    public ISubCategoryService subCategory { get; }
    public IPTAService PTA { get; }
    public IVaraityGroupService variantGroup { get; }
    public IEAVService EAV { get; }
    public IValueService values { get; }
    public IAttributeService attribute { get; } = null!;
    public IProductService product { get; } = null!;
    public IOrderItemService orderitem { get; } = null!;
    public IRatingService rating { get; }
    public IBrandService brand { get; }
    public IWishListService wishList { get; }
    public IAuthService auth { get; } = null!;
    public IUserService user { get; }


    private readonly IUnitRepository _unitrepository;

    public async Task saveChanges()
    {
        await _unitrepository.SaveChanges();
    }
}
