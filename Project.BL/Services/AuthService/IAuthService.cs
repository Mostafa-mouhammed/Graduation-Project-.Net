using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.Users;
using Project.DAL.Models;
using System.Security.Claims;

namespace Project.BL.Services.AuthService;
public interface IAuthService
{
    Task<StatuscodeDTO> registration(SignupDTO user);
    Task<StatuscodeDTO> Login(LoginDTO user);
}
