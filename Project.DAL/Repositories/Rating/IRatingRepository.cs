using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Ratingrepo;
public interface IRatingRepository:IGenericRepository<Rating>
{
    Task<Rating?>? getRating(int productId, string userId);
    Task<IEnumerable<Rating>> getProductRatings(int productId, int page,string userId);
    Task<double> getAvrageRate(int productId);
    Task<int> getRateCount(int productId);
}
