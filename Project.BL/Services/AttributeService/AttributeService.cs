
using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;


namespace Project.BL.Services.AttributeService;
public class AttributeService : IAttributeService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public AttributeService(IUnitRepository unit,IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO> AddAttribute(AttributeInsertDTO insert)
    {

        Attributes Exeistattributes = await _unit.attribute.getAttrbuitebyName(insert.Name);
        if (Exeistattributes != null)
            return new StatuscodeDTO(Statuscode.BadRequest,"There is already attribute with this name");

        Attributes attributes = _mapper.attribute.insertToModel(insert);
        await _unit.attribute.Add(attributes);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created,null,attributes);
    }

    public async Task<StatuscodeDTO> deleteAttribute(int Id)
    {
        Attributes Exeistattributes = await _unit.attribute.Getone(Id)!;
        if (Exeistattributes == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no attribute with this id");

        _unit.attribute.Delete(Exeistattributes);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> GetAllAttributes()
    {
        IEnumerable<Attributes> attributes = await _unit.attribute.GetAll();
        IEnumerable<AttributesReadDTO> attributesReadDTO = _mapper.attribute.modelToReadList(attributes);

        return new StatuscodeDTO(Statuscode.Ok,null, attributesReadDTO);
    }

    public async Task<StatuscodeDTO> updateAttribute(int Id, AttributeUpdateDTO update)
    {
        Attributes Exeistattributes = await _unit.attribute.Getone(Id)!;
        if (Exeistattributes == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no attribute with this id");

        Exeistattributes.Name = update.Name;
        _unit.attribute.Update(Exeistattributes);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }
}
