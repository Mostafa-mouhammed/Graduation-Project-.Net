using Project.BL.Dtos.Cart;
using Project.BL.Dtos.CartProduct;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.Security.Claims;

namespace Project.BL.Services.CartService;
public class CartService : ICartService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public CartService(IUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO> addToCart(ClaimsPrincipal user, CartProductInsertDTO insert)
    {
        User? exiestUser = await _unitRepository.user.GetUserAsync(user);
        if (exiestUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "invalid Token");

        Cart? cart = await _unitRepository.cart.getCartByUserId(exiestUser.Id);
        if (cart == null)
        {
            createCart(exiestUser.Id);
            return new StatuscodeDTO(Statuscode.NotFound, "you don't have a cart but now you do try again");
        }

        Product? product = await _unitRepository.product.Getone(insert.ProductId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        if (product.Quantity < insert.CartProductQuantity)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no enough qunatity in the stock");

        if (insert.CartProductQuantity <= 0)
            return new StatuscodeDTO(Statuscode.BadRequest, "quantity must be more than zero");

        CartProducts? cartProduct = await _unitRepository.cartProduct.getCartProductByProductId(insert.ProductId, cart.Id);
        if (cartProduct != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "this product is already in your cart");

        CartProducts cartProducts = _mapper.cartProduct.insertToModel(insert, cart.Id);
        await _unitRepository.cartProduct.Add(cartProducts);
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.Ok, null, insert);
    }

    public async Task<StatuscodeDTO> updateToCart(ClaimsPrincipal user, CartProductInsertDTO insert)
    {
        User? exiestUser = await _unitRepository.user.GetUserAsync(user);
        if (exiestUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "invalid Token");

        Cart? cart = await _unitRepository.cart.getCartByUserId(exiestUser.Id);
        if (cart == null)
        {
            createCart(exiestUser.Id);
            return new StatuscodeDTO(Statuscode.NotFound, "you don't have a cart but now you do try again");
        }

        Product? product = await _unitRepository.product.Getone(insert.ProductId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        if (product.Quantity < insert.CartProductQuantity)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no enough qunatity in the stock");

        if (insert.CartProductQuantity <= 0)
            return new StatuscodeDTO(Statuscode.BadRequest, "quantity must be more than zero");

        CartProducts? cartProduct = await _unitRepository.cartProduct.getCartProductByProductId(insert.ProductId, cart.Id);
        if (cartProduct == null)
            return new StatuscodeDTO(Statuscode.NotFound, "this product is not in your cart");

        cartProduct.CartProductQuantity = insert.CartProductQuantity;
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.Ok, null, insert);
    }

    public async Task<StatuscodeDTO> clearCart(ClaimsPrincipal user)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "invalid Token");

        Cart? cart = await _unitRepository.cart.getCartByUserId(existedUser.Id);
        if (cart == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no cart with this id");

        _unitRepository.cartProduct.deleteAllbyCartId(cart.Id);
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async void createCart(string userId)
    {
        Cart createdCart = new Cart()
        {
            UserId = userId,
        };
        await _unitRepository.cart.Add(createdCart);
        await _unitRepository.SaveChanges();
    }

    public async Task<StatuscodeDTO> deleteProductinCart(ClaimsPrincipal user, int productId)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Cart? cart = await _unitRepository.cart.getCartByUserId(existedUser.Id);
        if (cart == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no cart with this id");

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        CartProducts? cartProduct = await _unitRepository.cartProduct.getCartProductByProductId(productId, cart.Id);
        if (cartProduct == null)
            return new StatuscodeDTO(Statuscode.NotFound, "This product is not in your cart");

        _unitRepository.cartProduct.deleteOneByProductId(productId, cart.Id);
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getCart(ClaimsPrincipal user)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "invalid Token");

        Cart? cart = await _unitRepository.cart.getCartByUserId(existedUser.Id);
        if (cart == null)
        {
            createCart(existedUser.Id);
            return new StatuscodeDTO(Statuscode.NotFound, "you don't have a cart but now you do try again");
        }
        CartReadDTO cartDTO = _mapper.cart.CartModelTORead(cart);

        return new StatuscodeDTO(Statuscode.Ok, null, cartDTO);
    }
}
