using Project.BL.Dtos.Product;

namespace Project.BL.Dtos.CartProduct;
public record CartProductReadDTO(int ProductId,ProductReadDTO product,int CartProductQuantity);