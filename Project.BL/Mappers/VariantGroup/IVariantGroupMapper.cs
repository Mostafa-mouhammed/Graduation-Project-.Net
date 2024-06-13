using Project.BL.Dtos.VariantGroup;
using Project.DAL.Models;

namespace Project.BL.Mappers.VariantGroupMapper;
public interface IVariantGroupMapper
{
    VariantGroupReadDTO modelToRead(VariantGroup model);
    IEnumerable<VariantGroupReadDTO> modelToReadList(IEnumerable<VariantGroup> model);
    VariantGroup insertToModel(VariantGroupInsertDto insert);
}
