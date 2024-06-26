using Project.BL.Dtos.Brand;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;

namespace Project.BL.Services.BrandService;
public class BrandService : IBrandService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public BrandService(IUnitRepository unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> addBrand(BrandInsertDTO insert)
    {
        Brand exiestBrand = await _unit.brand.getByName(insert.Name);
        if (exiestBrand != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "This brand is already exiest");

        Brand brand = await _mapper.brand.insertToModel(insert);
        BrandReadDTO brandReadDTO = _mapper.brand.modelToRead(brand);

        _unit.brand.Add(brand);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Ok, null, brandReadDTO);
    }

    public async Task<StatuscodeDTO> deleteBrand(int id)
    {
        Brand? brand = await _unit.brand.Getone(id);
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "This is no brand with this id");

        //_unit.brand.Delete(brand);
        brand.isDeleted = true;
        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getAdminBrands()
    {
        IEnumerable<Brand> brands = await _unit.brand.GetAll();
        IEnumerable<BrandAdminDTO> brandsDTO = _mapper.brand.modelToAdminReadList(brands);
        return new StatuscodeDTO(Statuscode.Ok, null, brandsDTO);
    }

    public async Task<StatuscodeDTO> getBrand(int id)
    {
        Brand? brand = await _unit.brand.Getone(id);
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no brand with this id");
        BrandReadDTO bradDTO = _mapper.brand.modelToRead(brand);
        return new StatuscodeDTO(Statuscode.Ok, null, bradDTO);
    }

    public async Task<StatuscodeDTO> getGeneralBrands()
    {
        IEnumerable<Brand> brands = await _unit.brand.getGeneralBrands();
        IEnumerable<BrandReadDTO> brandsDTO = _mapper.brand.modelToReadList(brands);
        return new StatuscodeDTO(Statuscode.Ok, null, brandsDTO);
    }

    public async Task<StatuscodeDTO> retrieveDeletedBrand(int id)
    {
        Brand? brand = await _unit.brand.Getone(id);
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no brand with this id");

        brand.isDeleted = false;
        _unit.product.retriveDeletedByBrand(brand.Id);

        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> softDeleteBrand(int id)
    {
        Brand? brand = await _unit.brand.Getone(id);
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no brand with this id");

        brand.isDeleted = true;
        _unit.product.softDeleteByBrand(brand.Id);

        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> updateBrand(int id, BrandInsertDTO insert)
    {
        Brand? brand = await _unit.brand.Getone(id);
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no brand with this id");

        brand.Name = insert.Name;
        if(insert.image != null) brand.image = insert.image;
        _unit.brand.Update(brand);
        await _unit.SaveChanges();

        BrandReadDTO bradDTO = _mapper.brand.modelToRead(brand);
        return new StatuscodeDTO(Statuscode.Ok, null, bradDTO);
    }
}