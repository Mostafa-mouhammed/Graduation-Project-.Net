using Project.BL.Dtos.EAVProducts;
using Project.BL.Dtos.Statuscode;
using Project.BL.Dtos.Values;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.Collections.Generic;

namespace Project.BL.Services.ValuesService;
public class ValuesService : IValueService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public ValuesService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> AddValues(ValueInsertDTO insert)
    {
        Values exiestedvalue = await _unit.values.getValuebyName(insert.Name);
        if (exiestedvalue != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is already value with this name");

        Attributes exiestedattribute = await _unit.attribute.Getone(insert.attributeId)!;
        if (exiestedattribute == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no attribute with this id");

        Values value =  _mapper.values.insertToModel(insert);

        await _unit.values.Add(value);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created);
    }

    public async Task<StatuscodeDTO> deleteValues(int Id)
    {
        Values exiestedvalue = await _unit.values.Getone(Id)!;
        if (exiestedvalue == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is already value with this name");

         _unit.values.Delete(exiestedvalue);
         await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> GetAllValuess()
    {
        IEnumerable<Values> values = await _unit.values.GetAll();
        IEnumerable<ValuesReadDTO> valuesReadDTOs = _mapper.values.modelToReadList(values);
        return new StatuscodeDTO(Statuscode.Ok,null, valuesReadDTOs);
    }

    public async Task<StatuscodeDTO> getValuesOfGroup(int Id)
    {
        VariantGroup variantGroup = await _unit.varaityGroup.Getone(Id);
        if (variantGroup == null)
            return new StatuscodeDTO(Statuscode.NotFound,"there is no group with this id");

        IEnumerable<EAVProducts> eAVProducts = await _unit.EAV.getEAVbyGroupId(Id);
        if (eAVProducts == null)
            return new StatuscodeDTO(Statuscode.NotFound, "there is no group with this id");

        IEnumerable<EAVReadDTO> eavDTO =  _mapper.EAV.modelToReadList(eAVProducts);
        return new StatuscodeDTO(Statuscode.Ok, null, eavDTO);

    }

    public async Task<StatuscodeDTO> updateValues(int Id, ValueUpdateDTO update)
    {
        Values exiestedvalue = await _unit.values.Getone(Id)!;
        if (exiestedvalue == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "There is already value with this name");

        exiestedvalue.Name = update.Name;
        exiestedvalue.attributeId = update.attributeId;
        _unit.values.Update(exiestedvalue);

        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }
}
