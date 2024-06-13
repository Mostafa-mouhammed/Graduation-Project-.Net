namespace Project.BL.Dtos.Product;
public record ProductGeneralPaginationDTO(IEnumerable<ProductReadDTO> products,int totalPages);