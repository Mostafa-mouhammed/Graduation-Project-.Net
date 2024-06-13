
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.WishListrepo;

public class WishListRepository : GenericRepository<WishList> , IWishListRepository
{
    public WishListRepository(APIContext context):base(context)
    {
        
    }

    public async Task<WishList>? getWishList(string userId, int productId)
    {
        IQueryable<WishList> query = _context
            .Set<WishList>()
            .Where(w => w.productId == productId && w.userId == userId)
            .AsQueryable();
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<int>> getWishListByUser(string userId)
    {
        IQueryable<int> query = _context
            .Set<WishList>()
            .Where(w => w.userId == userId)
            .Select(w => w.productId)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Product>> getWishListByUserWithProducts(string userId)
    {
        IQueryable<Product> query = _context
           .Set<WishList>()
           .Where(w => w.userId == userId)
           .Include(w => w.product)
           .Select(w => w.product)
           .AsQueryable();

        return await query.ToListAsync();
    }
}
