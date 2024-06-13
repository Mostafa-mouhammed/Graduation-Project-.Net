using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.Users;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.Security.Claims;

namespace Project.BL.Services.UserService;

public class UserService : IUserService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public UserService(IUnitRepository unit,IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO> getUser(ClaimsPrincipal user)
    {
        User? exiestUser = await _unit.user.GetUserAsync(user);
        if (exiestUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "invalid Token");

        UserReadDTO userRead = _mapper.user.modelToRead(exiestUser);
        return new StatuscodeDTO(Statuscode.Ok,null, userRead);
    }

    public async Task<StatuscodeDTO> updatePassword(ClaimsPrincipal user, UserPasswordUpdateDTO password)
    {
        User? Existuser = await _unit.user.GetUserAsync(user);
        if (Existuser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "email or password is incorrect");

        bool isPasswordMatch = await _unit.user.CheckPasswordAsync(Existuser, password.oldpassword);
        if (!isPasswordMatch)
            return new StatuscodeDTO(Statuscode.NotFound, "password is incorrect");

        await _unit.user.ChangePasswordAsync(Existuser,password.oldpassword,password.newpassword);

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> updateProfileImage(ClaimsPrincipal user, IFormFile newphoto)
    {
        User? Existuser = await _unit.user.GetUserAsync(user);
        if (Existuser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "email or password is incorrect");

        string imageUrl = await _mapper.image.ConvertImage(newphoto);
        Existuser.image = imageUrl;
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> updateUser(ClaimsPrincipal user, UpdateUserDTO update)
    {
        User? Existuser = await _unit.user.GetUserAsync(user);
        if (Existuser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "email or password is incorrect");

        Existuser.firstName = update.firstname;
        Existuser.lastName = update.lastname;
        Existuser.Email = update.email;
        Existuser.address = update.address;
        await _unit.user.UpdateAsync(Existuser);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }
}
