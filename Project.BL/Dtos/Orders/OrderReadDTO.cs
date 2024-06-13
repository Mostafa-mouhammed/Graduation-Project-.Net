using Project.BL.Dtos.OrderItem;
using Project.DAL.Models;
using System.Text.Json.Serialization;

namespace Project.BL.Dtos.Orders;

public record OrderReadDTO
{
    public int Id { get; init; }
    public DateTime ShippingDate { get; init; } = DateTime.UtcNow;
    public DateTime DeliverDate { get; init; } = DateTime.UtcNow.AddDays(3);
    public string ShippingAddress { get; init; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus status { get; init; } 
    public decimal total { get; init; } 
    public IEnumerable<OrderItemReadDTO> Items { get; init; } = null!;
}
