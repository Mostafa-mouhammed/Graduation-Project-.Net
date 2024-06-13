using Project.BL.Dtos.CartProduct;
using Project.DAL.Models;

namespace Project.BL.Dtos.Cart;
public record CartReadDTO(int Id, string UserId, IEnumerable<CartProductReadDTO> Products);