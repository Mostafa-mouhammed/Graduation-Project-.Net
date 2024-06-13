using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.VariantGroup;
using Project.BL.Dtos.VariantGroupAttributes;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.VaraityGroup;
public class VaraityGroupService : IVaraityGroupService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public VaraityGroupService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> addOne(VariantGroupInsertDto insert)
    {

        if(!insert.attributesIds.Any())
            return new StatuscodeDTO(Statuscode.BadRequest, "you must insert attributes to this group");

        VariantGroup? variantGroup = await _unit.varaityGroup.getGroupbyName(insert.Name)!;
        if (variantGroup != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is already group exiest with this name");

        if(!await checkAllAtrributeExeist(insert.attributesIds))
            return new StatuscodeDTO(Statuscode.NotFound, "some attributes is not exesit");


        VariantGroup variantGroupModel = _mapper.variantGroup.insertToModel(insert);
        await _unit.varaityGroup.Add(variantGroupModel);
        await _unit.SaveChanges();

        IEnumerable<VariantGroupAttributes> variantGroupAttributes =
            _mapper.VariantGroupAttribute.insertToModelList(variantGroupModel.Id,insert.attributesIds);

        await _unit.variantGroupAttribute.AddRange(variantGroupAttributes);
        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    private async Task<bool> checkAllAtrributeExeist(IEnumerable<int> atrributesIds)
    {
        IEnumerable<int> existedIds = await _unit.attribute.checkAllAttributesExiest(atrributesIds);
        return atrributesIds.Count() == existedIds.Count();
    }

    public async Task<StatuscodeDTO> delete(int Id)
    {
        VariantGroup variantGroup = await _unit.varaityGroup.Getone(Id)!;
        if (variantGroup == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no group with this id");

        _unit.varaityGroup.Delete(variantGroup);
        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getAll()
    {
        IEnumerable<VariantGroup> variantGroups = await _unit.varaityGroup.GetAll();
        IEnumerable<VariantGroupReadDTO> variantGroupsDTO = _mapper.variantGroup.modelToReadList(variantGroups);
        return new StatuscodeDTO(Statuscode.Ok, null, variantGroupsDTO);
    }

    public async Task<StatuscodeDTO> getOne(int id)
    {
        VariantGroup variantGroup = await _unit.varaityGroup.Getone(id)!;
        if (variantGroup == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no group with this id");

        VariantGroupReadDTO groupReadDTO = _mapper.variantGroup.modelToRead(variantGroup);

        return new StatuscodeDTO(Statuscode.Ok,null, groupReadDTO);
    }

    public async Task<StatuscodeDTO> getOnebyName(string name)
    {
        VariantGroup? variantGroup = await _unit.varaityGroup.getGroupbyName(name)!;
        if (variantGroup == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no group with this name");

        VariantGroupReadDTO groupReadDTO = _mapper.variantGroup.modelToRead(variantGroup);

        return new StatuscodeDTO(Statuscode.Ok, null, groupReadDTO);
    }

    public async Task<StatuscodeDTO> update(int Id, VariantGroupUpdate update)
    {
        VariantGroup variantGroup = await _unit.varaityGroup.Getone(Id)!;
        if (variantGroup == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no group with this id");

        variantGroup.Name = update.Name;
        _unit.varaityGroup.Update(variantGroup);
        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getGroupAttributesValues(int id)
    {
        IEnumerable<VariantGroupAttributes> variantGroup = await _unit.variantGroupAttribute.getGroupAttributeValues(id);
        VariantGroupAttributeReadDTO variantGroupDTO = _mapper.VariantGroupAttribute.modelToRead(variantGroup);
        return new StatuscodeDTO(Statuscode.Ok,null, variantGroupDTO);
    }
}
