using Microsoft.IdentityModel.Tokens;
using Project.BL.Dtos.Attribute;
using Project.BL.Dtos.EAVProducts;
using Project.BL.Dtos.Product;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.Linq;

namespace Project.BL.Services.ProductService;
public class ProductService : IProductService
{
    private readonly IUnitRepository _unit;
    private readonly IMapper _mapper;

    public ProductService(IUnitRepository unitRepository, IMapper mapper)
    {
        _unit = unitRepository;
        _mapper = mapper;
    }


    public async Task<StatuscodeDTO> GenOneProduct(int id)
    {
        Product? productModel = await _unit.product.GetOneProduct(id);
        if (productModel == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        if(productModel.Ratings.Any()) 
            productModel.rate = productModel.Ratings.Average(r => r.rate);


        IEnumerable<EAVProducts> eAVProducts = await _unit.EAV.getEAVbyGroupId(productModel.variantGroupId??0);

        ProductOneDTO OneProduct = _mapper.product.modelToOneOnlyRead(productModel, eAVProducts);
        return new StatuscodeDTO(Statuscode.Ok, null, OneProduct);
    }

    public async Task<StatuscodeDTO> AddSimpleProduct(ProductSimpleInsertDTO insert)
    {
        /* checking for category and brand is exeisting to link them with the product */
        SubCategory? subCategory = await _unit.subCategory.Getone(insert.subCategoryId)!;
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no sub category with this id");

        Brand? brand = await _unit.brand.Getone(insert.brandId)!;
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no brand with this id");

        Product productModel = await _mapper.product.insertSimpleToModel(insert);

        /* Save product in database and get the id created */
        await _unit.product.Add(productModel);
        await _unit.SaveChanges();

        /* convert the product images from insert IFormFiles to IEnumerable<ProductsImages> linked to productId */
        IEnumerable<ProductsImages> productsImages =
             _mapper.productImages.insertToModelList(insert.productImages,  productModel.Id);

        /* save the product images to database and save changes */
        await _unit.productImages.AddRange(productsImages);

        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created, null);
    }

    public async Task<StatuscodeDTO> AddVarProduct(ProductVarInsertDTO insert)
    {
        /* check if inserted values is empty or not */
        if (!insert.values.Any())
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no values inserted");

        /* check if there is images or product id to clone it's images for this product */
        if (insert.ExiestImagesProductId == null && (!insert.productImages.Any() || insert.Image == null))
            return new StatuscodeDTO(Statuscode.BadRequest, "There is no images nor product id");

        SubCategory? subCategory = await _unit.subCategory.Getone(insert.subCategoryId)!;
        if (subCategory == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        Brand? brand = await _unit.brand.Getone(insert.brandId)!;
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no brand with this id");

        VariantGroup? variantGroup = await _unit.varaityGroup.getGroupWithAttributes(insert.varGroupId)!;
        if (variantGroup == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no variant Group with this id");

        IEnumerable<Values> values = await _unit.values.getValuesInList(insert.values);

        bool isValuesExeist = await isAllValuesValid(insert.values, values);
        if (!isValuesExeist)
            return new StatuscodeDTO(Statuscode.BadRequest, "some or all values are not valid");


        bool isAllGroupValuesUnique = await isAllValuesnotDuplicated(insert.values,insert.varGroupId);
        if (isAllGroupValuesUnique)
            return new StatuscodeDTO(Statuscode.BadRequest, "inserted values are dublicated");

        bool isGroupAttributeMatchesValuesAtribute = await isValuesMatchesGroupAttributes(variantGroup,values);
        if (!isGroupAttributeMatchesValuesAtribute)
            return new StatuscodeDTO(Statuscode.BadRequest, "values attribute must matches the group attribute");

        Product productModel = await _mapper.product.insertVarToModel(insert);
        await _unit.product.Add(productModel);
        await _unit.SaveChanges();

        /* convert the product images from insert IFormFiles to IEnumerable<ProductsImages> linked to productId */
        if (!insert.productImages.IsNullOrEmpty())
        {
            IEnumerable<ProductsImages> productsImages =
             _mapper.productImages.insertToModelList(insert.productImages, productModel.Id);

            /* save the product images to database and save changes */
            await _unit.productImages.AddRange(productsImages);
        }
        else
        {
            Product product = await _unit.product.Getone(insert.ExiestImagesProductId ?? 0)!;
            if(product == null)
                return new StatuscodeDTO(Statuscode.NotFound, "there is no product exiest with this id");

            if (product.variantGroupId != insert.varGroupId)
                return new StatuscodeDTO(Statuscode.BadRequest, "product id is assign to diffrent group");

            /* clone product images to the new product */
            productModel.Image = product.Image;
            await cloneProductImages(product.Id, productModel.Id);
        }


        IEnumerable<EAVProducts> EAVModelList = _mapper.EAV.insertToModelList(insert.values, insert.varGroupId, productModel.Id);

        await _unit.EAV.AddRange(EAVModelList);
        await _unit.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created, null);
    }


    private async Task<bool> isAllValuesValid(IEnumerable<int> valueList,IEnumerable<Values> insertedValueList)
    {
        IEnumerable<int> InsertedvaluesIds = insertedValueList.Select(x => x.Id);
        bool isAllExeist = valueList.All(l => InsertedvaluesIds.Contains(l));
        return isAllExeist;
    }

    public async Task<StatuscodeDTO> UpdateProduct(int id, ProductSimpleInsertDTO insert)
    {
        Product? product = await _unit.product.Getone(id)!;
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        Category? category = await _unit.category.Getone(insert.subCategoryId)!;
        if (category == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no category with this id");

        Brand? brand = await _unit.brand.Getone(insert.brandId)!;
        if (brand == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no brand with this id");

        /* Updating product properties */
        product.Name = insert.Name;
        product.Price = insert.Price;
        product.subCategoryId = insert.subCategoryId;
        if (insert.Image != null) product.Image = insert.Image;
        product.Quantity = insert.Quantity;
        product.brandId = insert.brandId;

        _unit.product.Update(product);
        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> DeleteProduct(int id)
    {
        Product? product = await _unit.product.Getone(id)!;
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        //_unit.product.Delete(product);
        product.isDeleted = true;
        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> GetGeneralProductsPagination
        (int page = 1, int limit = 20, string sort = "Default", int categoryId = 0, int brandId = 0)
    {
        IEnumerable<Product>? productModelList = await _unit.product
            .GetProductsGeneralPagination(page, limit, sort, categoryId, brandId);
        CalculateAvgRating(productModelList!);

        IEnumerable<ProductReadDTO>? productReadDTOs = _mapper.product.listModelToReadDTO(productModelList!);

        int countItems = await _unit.product.GetTotalGeneralPagination(categoryId, brandId);
        int totalPages = getTotalPages(countItems, limit);

        ProductGeneralPaginationDTO productByPagination = _mapper.product.toGeneralPagination(productReadDTOs, totalPages);

        return new StatuscodeDTO(Statuscode.Ok, null, productByPagination);
    }

    public async Task<StatuscodeDTO> GetAdminProductsPagination
        (int page = 1, int limit = 20, string sort = "Default", int categoryId = 0, int brandId = 0)
    {
        IEnumerable<Product>? productsModelList = await _unit.product
            .GetProductsAdminPagination(page, limit, sort, categoryId, brandId);

        CalculateAvgRating(productsModelList!);

        IEnumerable<ProductAdminReadDTO> productAdminReadDTOs = _mapper.product.listModelToAdminDTO(productsModelList!);

        int countItems = await _unit.product.GetTotalAdminPagination(categoryId, brandId);
        int totalPages = getTotalPages(countItems, limit);
        ProductAdminPaginationDTO productByPagination = _mapper.product.toAdminPagination(productAdminReadDTOs, totalPages);

        return new StatuscodeDTO(Statuscode.Ok, null, productByPagination);
    }

    int getTotalPages(int count, int limit)
    {
        return (int)Math.Ceiling(((double)count / limit));
    }
    private void CalculateAvgRating(IEnumerable<Product> products)
    {
        int count = products.Count();
        foreach (Product product in products)
        {
            product.rate = product.Ratings.Count() > 0 ? product.Ratings.Average(r => r.rate) : 0;
        }
    }

    public async Task<StatuscodeDTO> RetriveDeleteProduct(int id)
    {
        Product? product = await _unit.product.Getone(id)!;
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        //_unit.product.Delete(product);
        product.isDeleted = false;
        await _unit.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);
    }

    /* function to clone images of product to another */
    private async Task cloneProductImages(int FromId,int ToId)
    {
        /* Get images from the product we want to clone */
        IEnumerable<ProductsImages> FromProductImages = await _unit.productImages.getProductImagesByProductId(FromId);

        /* Create a new list of product images and change the Id of the Product Id with the same images */
        IEnumerable<ProductsImages> clonedProductImages = _mapper.productImages.changeProductId(FromProductImages,ToId);

        /* Adding the new list of product images assign them to the new product */
        await _unit.productImages.AddRange(clonedProductImages);
    }
    private async Task<bool> isValuesMatchesGroupAttributes(VariantGroup group, IEnumerable<Values> values)
    {
        IEnumerable<int> groupAttributesIds = group.variantGroupAttributes.Select(vg => vg.attributeId);
        IEnumerable<int> valuesAttributesIds = values.Select(vg => vg.attributeId);

        bool allmatches = groupAttributesIds.All(g => valuesAttributesIds.Contains(g));

        if (groupAttributesIds.Count() != valuesAttributesIds.Count() || !allmatches)
            return false;

        return true;
    }
    private async Task<bool> isAllValuesnotDuplicated(IEnumerable<int> valuesIds,int groupId)
    {
        bool isTypicalValues = false;
        /* get all eav by the group id inserted */
        IEnumerable<EAVProducts> EAVListbyGroup = await _unit.EAV.getEAVbyGroupId(groupId);

        /* get list of unique products ids of this group id */
        IEnumerable<int> productsIds = EAVListbyGroup
            .DistinctBy(e => e.productId)
            .Select(e => e.productId)
            .ToList();

        /* loop through the products ids of this group */
        foreach (var id in productsIds)
        {
        /* get the values in each itiration for each product of the group */
            IEnumerable<int> valueIdsbyproduct = EAVListbyGroup
                .Where(e => e.productId == id)
                .Select(e => e.valueId);

        /* check if all values of this product match the inserted list of values */
             isTypicalValues = valuesIds.All(e => valueIdsbyproduct.Contains(e));
        /* if true (matches) stop the loop, it can't be added to prevent dublication of rows */
            if (isTypicalValues) break;

        }

        return isTypicalValues;
    }


}
