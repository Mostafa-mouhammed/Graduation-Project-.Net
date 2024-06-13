using Microsoft.EntityFrameworkCore;
using Project.DAL.Data;
using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Ratingrepo;
public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(APIContext context):base(context)
    {
        
    }

    public async Task<double> getAvrageRate(int productId)
    {
       IQueryable<int> query = _context
            .Set<Rating>()
            .Where(r => r.productId == productId)
            .Select(r => r.rate)
            .AsQueryable();

        return await query
            .DefaultIfEmpty()
            .AverageAsync();
    }

    public async Task<int> getRateCount(int productId)
    {
        IQueryable<int> query = _context
            .Set<Rating>()
            .AsNoTracking()
            .Where(r => r.productId == productId)
            .Select(r => r.productId)
            .AsQueryable();

        return await query.CountAsync();
    }

    public async Task<IEnumerable<Rating>> getProductRatings(int productId,int page,string userId = "")
    {
        IQueryable<Rating> query =_context
            .Set<Rating>()
            .Where(r => r.productId == productId)
            .Where(r => r.userId != userId)
            .Include(r => r.user)
            .Skip((page - 1) * 5)
            .Take(5)
            .AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<Rating?> getRating(int productId, string userId ="")
    {
        IQueryable<Rating> query = _context
            .Set<Rating>()
            .Where(r => r.productId == productId && r.userId == userId)
            .Include(r => r.user)
            .AsQueryable();

        return await query.FirstOrDefaultAsync();
    }


}
