using Project.BL.Dtos.VariantGroup;
using Project.DAL.Models;

namespace Project.BL.Mappers.VariantGroupMapper;
public class VariantGroupMapper : IVariantGroupMapper
{
    public VariantGroup insertToModel(VariantGroupInsertDto insert)
    {
        return new VariantGroup()
        {
            Name = insert.Name,
        };
    }

    public VariantGroupReadDTO modelToRead(VariantGroup model)
    {
        return new VariantGroupReadDTO(model.Id, model.Name);
    }

    public IEnumerable<VariantGroupReadDTO> modelToReadList(IEnumerable<VariantGroup> modelList)
    {
        return modelList.Select(m => modelToRead(m));
    }
}
