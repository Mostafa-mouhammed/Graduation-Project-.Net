using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;
using System.Linq;

namespace Project.DAL.Repositories.OrderItems;

public class OrderItemRepository:GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(APIContext context):base(context) { }

    public void addOrderItemsList(IEnumerable<OrderItem> model)
    {
        _context.orderItems.AddRangeAsync(model);
    }


    public async Task<IEnumerable<OrderItem>> getOrderItemsWithProducts(int orderId)
    {
        return await _context.orderItems
              .Where(oi => oi.OrderId == orderId)
              .Include(oi => oi.product)
              .ToListAsync();
    }

    public async Task<bool> ifUserPurchasedProduct(string userId, int productId)
    {
       return await _context.orderItems
            .Where(oi => oi.order.UserId == userId && oi.ProductId == productId)
            .AnyAsync();
    }

    public async Task<int?> isProductEligible(int productId, string userId)
    {
        IQueryable<int> query = _context
            .Set<OrderItem>()
            .Where(o => o.ProductId == productId)
            .Where(o => o.order.status == OrderStatus.Delivered)
            .Include(o => o.order)
            .Where(o => o.order.UserId == userId)
            .Select(o => o.ProductId)
            .AsQueryable();

        return await query.FirstOrDefaultAsync();

    }
}
