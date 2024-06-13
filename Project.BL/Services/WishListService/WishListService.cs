using Project.BL.Dtos.Product;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.WishList;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.Security.Claims;

namespace Project.BL.Services.WishListService;
public class WishListService : IWishListService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public WishListService(IUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> addToWishList(ClaimsPrincipal user, int productId)
    {
        User? exiestUser = await _unitRepository.user.GetUserAsync(user);
        if (exiestUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "This product is not exeist");

        WishList? exiestWishList = await _unitRepository.wishlist.getWishList(exiestUser.Id, productId);
        if (exiestWishList != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "This product is already in your wishlist");

        WishList wishList = _mapper.wishList.insertToModel(exiestUser.Id, productId);
        await _unitRepository.wishlist.Add(wishList);
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.Created);
    }

    public async Task<StatuscodeDTO> getWishListByUser(ClaimsPrincipal user)
    {
        User? exiestUser = await _unitRepository.user.GetUserAsync(user);
        if (exiestUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        IEnumerable<int> wishlist = await _unitRepository.wishlist.getWishListByUser(exiestUser.Id);
        return new StatuscodeDTO(Statuscode.Ok,null,wishlist);
    }

    public async Task<StatuscodeDTO> getWishListByUserWithProducts(ClaimsPrincipal user)
    {
        User? exiestUser = await _unitRepository.user.GetUserAsync(user);
        if (exiestUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        IEnumerable<Product> WishListProducts = await _unitRepository
            .wishlist.getWishListByUserWithProducts(exiestUser.Id);

        IEnumerable<ProductReadDTO> productReadDTO = _mapper.product.listModelToReadDTO(WishListProducts);

        return new StatuscodeDTO(Statuscode.Ok, null, productReadDTO);
    }

    public async Task<StatuscodeDTO> removeFromWishList(ClaimsPrincipal user, int productId)
    {
        User? exiestUser = await _unitRepository.user.GetUserAsync(user);
        if (exiestUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "This product is not exeist");

        WishList? exiestWishList = await _unitRepository.wishlist.getWishList(exiestUser.Id, productId);
        if (exiestWishList == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "This product is not in your wishlist");

        _unitRepository.wishlist.Delete(exiestWishList);
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }
}
