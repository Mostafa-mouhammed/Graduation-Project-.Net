using Project.BL.Dtos.Product;

namespace Project.BL.Dtos.Brand;
public record BrandReadWithProductDTO(int Id, string Name, IEnumerable<ProductReadDTO> products);