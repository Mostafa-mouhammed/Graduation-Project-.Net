namespace Project.BL.Dtos.Product;
public record ProductAdminPaginationDTO(IEnumerable<ProductAdminReadDTO> products, int totalPages);