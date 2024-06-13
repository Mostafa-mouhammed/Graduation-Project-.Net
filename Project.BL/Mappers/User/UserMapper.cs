using Microsoft.AspNetCore.Identity;
using Project.BL.Dtos.Users;
using Project.DAL.Models;

namespace Project.BL.Mappers.Users;
public class UserMapper : IUserMapper
{
    public UserReadDTO modelToRead(User user)
    {
        return new UserReadDTO(user.Id,user.firstName,user.lastName,user.Email,user.address,user.image);
    }

    public User? SignToUser(SignupDTO sign)
    {
        return new User()
        {
            firstName = sign.FirstName,
            lastName = sign.LastName,
            Email = sign.Email,
            UserName = sign.Email
        };
    }
}
