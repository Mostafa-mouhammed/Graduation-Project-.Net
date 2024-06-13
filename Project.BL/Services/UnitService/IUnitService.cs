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

namespace Project.BL.Services.UnitService;

public interface IUnitService
{
    public ICartProductService cartproduct { get; }
    public ICartService cart {get;}
    public ISubCategoryService subCategory {get;}
    public IAttributeService attribute {get;}
    public IProductTypesService productTypes {get;}
    public IVaraityGroupService variantGroup {get;}
    public IPTAService PTA {get;}
    public IImageService image {get;}
    public IEAVService EAV {get;}
    public IValueService values {get;}
    public ICategoryService categories {get;}
    public IOrderService order {get;}
    public IProductService product {get;}
    public IOrderItemService orderitem {get;}
    public IAuthService auth {get;}
    public IUserService user {get;}
    public IRatingService rating {get;}
    public IBrandService brand {get;}
    public IWishListService wishList {get;}
    public Task saveChanges();

}
