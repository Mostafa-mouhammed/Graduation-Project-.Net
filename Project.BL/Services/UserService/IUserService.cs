using Microsoft.AspNetCore.Http;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.Users;
using System.Security.Claims;

namespace Project.BL.Services.UserService;

public interface IUserService
{
    Task<StatuscodeDTO> getUser(ClaimsPrincipal user);
    Task<StatuscodeDTO> updateUser(ClaimsPrincipal user, UpdateUserDTO update);
    Task<StatuscodeDTO> updatePassword(ClaimsPrincipal user,UserPasswordUpdateDTO password);
    Task<StatuscodeDTO> updateProfileImage(ClaimsPrincipal user,IFormFile newphoto);
    Task<StatuscodeDTO> GetUserById(string id);
    Task<StatuscodeDTO> GetAllUsers();


}
