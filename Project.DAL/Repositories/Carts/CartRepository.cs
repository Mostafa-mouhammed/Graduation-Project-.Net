using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Carts;

public class CartRepository:GenericRepository<Cart>, ICartRepository
{
    public CartRepository(APIContext context):base(context){}

    public async Task<Cart?> getCartByUserId(string userid)
    {
        IQueryable<Cart> query = _context.Set<Cart>()
                 .AsNoTracking()
                 .Where(c => c.UserId == userid)
                 .Include(c => c.cartProducts)
                 .ThenInclude(p => p.Product)
                 .AsQueryable();

        return await query.FirstOrDefaultAsync();

    }
}
