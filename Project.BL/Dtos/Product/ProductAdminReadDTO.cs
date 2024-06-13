namespace Project.BL.Dtos.Product;
public record ProductAdminReadDTO(int Id, string Name, string desctiption, string Image, int Quantity, int Discount, double Price,bool isDeleted,double rate, int Categoryid, int brandId);
