using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project.DAL.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime ShippingDate { get; set; } = DateTime.UtcNow;
    public DateTime DeliverDate { get; set; } = DateTime.UtcNow.AddDays(3);
    public string ShippingAddress { get; set; } = string.Empty;
    public decimal totalprice { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public OrderStatus status { get; set; } = OrderStatus.PendingPayment;

    [ForeignKey("user")]
    public string UserId { get; set; } = string.Empty;
    public User user { get; set; } = null!;
    public IEnumerable<OrderItem> Items { get; set; } = null!;
}

public enum OrderStatus
{
    PendingPayment = 0,
    Cancelled = 1,
    Pending = 2,
    Deliverd = 3
}
