using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.Carts;

public interface ICartRepository:IGenericRepository<Cart>
{
    Task<Cart>? getCartByUserId(string userid);
}
