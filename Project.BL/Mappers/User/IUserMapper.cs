using Microsoft.AspNetCore.Identity;
using Project.BL.Dtos.Users;
using Project.DAL.Models;

namespace Project.BL.Mappers.Users;
public interface IUserMapper
{
    User? SignToUser(SignupDTO sign);
    UserReadDTO modelToRead(User user);
}
