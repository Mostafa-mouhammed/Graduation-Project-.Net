using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.BL.ExtensionServices;
using Project.DAL.Data;
using Project.DAL.ExtensionServices;
using Project.DAL.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Repository Services
builder.Services.AddDALServices(builder.Configuration);

// Mapper Services
builder.Services.AddMapperServices();

// Service Services
builder.Services.AddBLServices();

builder.Services.AddControllers();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<APIContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer("Data Source=SQL8005.site4now.net;Initial Catalog=db_aa9e4e_itigalaxywebapp1;User Id=db_aa9e4e_itigalaxywebapp1_admin;Password=A123456a"));


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
        ValidateAudience = false,
        IssuerSigningKey = symmetricKey,
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseStaticFiles();

// Use the CORS policy
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

app.Run();
