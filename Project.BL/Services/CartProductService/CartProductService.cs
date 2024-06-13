using Project.BL.Dtos.Cart;
using Project.BL.Dtos.CartProduct;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.Security.Claims;

namespace Project.BL.Services.CartProductService;

public class CartProductService : ICartProductService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public CartProductService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO> addLocalCart(ClaimsPrincipal user, IEnumerable<CartProductInsertDTO> insert)
    {
        User? exestedUser = await _unit.user.GetUserAsync(user);
        if (exestedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Cart cart = await _unit.cart.getCartByUserId(exestedUser.Id);
        if (cart == null)
            return new StatuscodeDTO(Statuscode.NotFound, "You don't have cart");

        await addEachCartItemLocally(insert, cart);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getCart(ClaimsPrincipal user)
    {
        User? exestedUser = await _unit.user.GetUserAsync(user);
        if (exestedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Cart cart = await _unit.cart.getCartByUserId(exestedUser.Id);
        if (cart == null)
            return new StatuscodeDTO(Statuscode.NotFound, "You don't have cart");

        CartReadDTO cartDTO = _mapper.cart.CartModelTORead(cart);
        return new StatuscodeDTO(Statuscode.Ok, null, cartDTO);

    }

    private async Task addEachCartItemLocally(IEnumerable<CartProductInsertDTO> insert, Cart cart)
    {
        IEnumerable<CartProducts> cartProductModel = _mapper.cartProduct.listInsertToModelDTO(insert, cart.Id);
        foreach (var item in cartProductModel)
        {
            bool exiest = cart.cartProducts.Select(p => p.ProductId).Contains(item.ProductId);
            if (exiest) continue;

            Product? product = await _unit.product.Getone(item.ProductId);
            if (product == null) continue;

            await _unit.cartProduct.Add(item);
        }
    }
}
