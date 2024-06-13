using Project.BL.Dtos.Product;
namespace Project.BL.Dtos.OrderItem;
public record OrderItemReadDTO(int Id, double ItemPrice, int ItemQuantitem, ProductReadDTO product);