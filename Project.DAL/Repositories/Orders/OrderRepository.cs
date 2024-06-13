using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;
using Stripe;
using Stripe.Checkout;

namespace Project.DAL.Repositories.Orders;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(APIContext context):base(context)
    {
        
    }

    public async Task<Order?> CheckOrder(string userId, int orderId)
    {
       return await _context.order
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
    }

    public void CreateOrder()
    {
        throw new NotImplementedException();
    }

    public async Task<Order?> GetOrder(int orderId)
    {
        return await _context.order
            .Where(o => o.Id == orderId)
            .Include(o => o.Items)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> getOrdersHistory(string userId, int page,string sort)
    {
        IQueryable<Order> query = _context
            .Set<Order>()
            .Where(o => o.UserId == userId)
            .Where(o => o.status != OrderStatus.PendingPayment)
            .Where(o => 
             sort.Equals("cancelled") ? o.status.Equals(OrderStatus.Cancelled)
            :sort.Equals("pending") ? o.status.Equals(OrderStatus.Pending)
            :sort.Equals("deliverd") ? o.status.Equals(OrderStatus.Deliverd)
            :!o.status.Equals(OrderStatus.PendingPayment)
            )
            .Skip((page - 1) * 5)
            .Take(5)
            .Include(o => o.Items)
            .ThenInclude(i => i.product)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<int> GetTotalPagesbyOrders(string userId)
    {
        return await _context.Set<Order>()
            .Where(o => o.UserId == userId)
            .Where(o => o.status != OrderStatus.PendingPayment)
            .CountAsync();
    }


}
