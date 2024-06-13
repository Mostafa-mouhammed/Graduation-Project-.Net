using Microsoft.AspNetCore.Http;
using Project.BL.Dtos.ProductImages;
using Project.DAL.Models;

namespace Project.BL.Mappers.ProductImages;
public interface IProductImagesMapper
{
    ProductImagesReadDTO modelToRead(ProductsImages model);
    IEnumerable<ProductImagesReadDTO> modelToReadList(IEnumerable<ProductsImages> model);

    ProductsImages insertToModel(string insert,int Id);
    IEnumerable<ProductsImages>insertToModelList(IEnumerable<string> insert, int Id);

    IEnumerable<ProductsImages> changeProductId(IEnumerable<ProductsImages> oldList, int newId);
}
