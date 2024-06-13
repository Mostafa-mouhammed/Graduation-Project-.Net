using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.BL.ExtensionServices;
using Project.DAL.Data;
using Project.DAL.ExtensionServices;
using Project.DAL.Models;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// repository Services
builder.Services.AddDALServices(builder.Configuration);

// mapper services
builder.Services.AddMapperServices();

// service services
builder.Services.AddBLServices();



builder.Services.AddControllers();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric=false;
    options.Password.RequireDigit=false;
    options.Password.RequireLowercase=false;
    options.Password.RequireUppercase=false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<APIContext>()
    .AddDefaultTokenProviders();


builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBSecretConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "defaultSchema";
    options.DefaultChallengeScheme = "defaultSchema";
}).AddJwtBearer("defaultSchema", options =>
{
    var key = builder.Configuration.GetValue<string>("jwtSecretKey");
    var keyInBytes = Encoding.ASCII.GetBytes(key!);
    var symmetricKey = new SymmetricSecurityKey(keyInBytes);

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience=false,
        IssuerSigningKey = symmetricKey,
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); 

app.UseAuthorization();
app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyOrigin();
    option.AllowAnyMethod();
});
app.MapControllers();


//using (var scope = app.Services.CreateScope())
//{
//    var rolemanger =  scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var roles = new[] { "Admin", "User" };
//    foreach (var role in roles)
//    {
//        if (!await rolemanger.RoleExistsAsync(role))
//            await rolemanger.CreateAsync(new IdentityRole(role));
//    }
//}

    app.Run();