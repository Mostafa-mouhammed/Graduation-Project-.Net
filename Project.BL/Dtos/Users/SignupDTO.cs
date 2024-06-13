using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Project.BL.Dtos.Users;

public record SignupDTO([MinLength(3)]string FirstName,
    [MinLength(3)] string LastName,
    [EmailAddress]string Email,
    string password,
    string? address);
