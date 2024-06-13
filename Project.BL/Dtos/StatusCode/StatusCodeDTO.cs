namespace Project.BL.Dtos.Statuscode;
public record StatuscodeDTO(Statuscode Statuscode,string? message = "",object? data = null);

public enum Statuscode
{
    BadRequest = 400, 
    NotFound = 404,
    Created = 201,
    Ok = 200,
    NoContent = 204,
    Redirect= 301
};