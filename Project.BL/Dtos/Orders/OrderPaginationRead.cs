namespace Project.BL.Dtos.Orders;
public record OrderPaginationRead(IEnumerable<OrderReadDTO> orders,int TotalPages);