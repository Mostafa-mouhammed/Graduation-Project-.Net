using System.ComponentModel.DataAnnotations;

namespace Project.BL.Dtos.Users;
public record UpdateUserDTO(
   [Required] [MinLength(2)] [MaxLength(25)] string firstname,
   [Required] [MinLength(2)][MaxLength(25)] string lastname,
   [Required][EmailAddress] string email,
   [MaxLength(400)] string address
    );
