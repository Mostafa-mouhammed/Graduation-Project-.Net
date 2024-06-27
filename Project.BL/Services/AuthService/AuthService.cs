using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.Users;
using Project.BL.Mappers.Mapper;
using Project.BL.Services.CartService;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.BL.Services.AuthService;
public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManger;
    private readonly ICartService _cartservice;
    private readonly IUnitRepository _unit;

    public AuthService(IConfiguration configuration, IMapper mapper, UserManager<User> userManger,ICartService cartservice,IUnitRepository unit)
    {
         _configuration = configuration;
        _mapper = mapper;
        _userManger = userManger;
        _cartservice = cartservice;
        _unit = unit;
    }
    private string generateToken(User user)
    {
        List<Claim> userClaims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name,user.fullName)
        };
        var roles =  _userManger.GetRolesAsync(user).Result.SingleOrDefault();
         if(roles != null)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, roles));
        }

        var key = _configuration.GetSection("jwtSecretKey").Value;
        var keyInBytes = Encoding.ASCII.GetBytes(key);
        var symitricKey = new SymmetricSecurityKey(keyInBytes);
        var signingCredentials = new SigningCredentials(symitricKey, SecurityAlgorithms.HmacSha256Signature);
        var expireDate = DateTime.UtcNow.AddDays(1);
        var jwt = new JwtSecurityToken(
            claims: userClaims,
            expires: expireDate,
            
            signingCredentials: signingCredentials);
        var Token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return Token;
    }

    public async Task<StatuscodeDTO> Login(LoginDTO login)
    {
        User? user = await _userManger.FindByEmailAsync(login.Email);
        if (user == null)
            return new StatuscodeDTO(Statuscode.NotFound,"email or password is incorrect");

        bool isPasswordMatch = await _userManger.CheckPasswordAsync(user, login.password);
        if (!isPasswordMatch)
            return new StatuscodeDTO(Statuscode.NotFound, "email or password is incorrect");

        string Token = generateToken(user);
        var x = await _userManger.GetRolesAsync(user);   
        return new StatuscodeDTO(Statuscode.Ok, null, Token);
    }

    public async Task<StatuscodeDTO> registration(SignupDTO user)
    {
        User? isUserExiest = await _userManger.FindByEmailAsync(user.Email);
        if (isUserExiest != null)
            return new StatuscodeDTO(Statuscode.BadRequest,"this email is already exiest");

        User? newUser = _mapper.user.SignToUser(user);
        var result = await _userManger.CreateAsync(newUser, user.password);


        var roleResult = await _userManger.AddToRoleAsync(newUser, "User");
        if (!roleResult.Succeeded)
            return new StatuscodeDTO(Statuscode.BadRequest, roleResult.Errors.ToString());



        string Token = generateToken(newUser);

        if (!result.Succeeded)  
            return new StatuscodeDTO(Statuscode.BadRequest, result.Errors.ToString());

        /* Assign a cart to the user */
        Cart newCart = _mapper.cart.insertToModel(newUser.Id);
        await _unit.cart.Add(newCart);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Ok,null,Token);
    }

} 
