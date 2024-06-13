using Microsoft.AspNetCore.Http;
using Project.BL.Dtos.ProductImages;
using Project.BL.Mappers.Images;
using Project.DAL.Models;

namespace Project.BL.Mappers.ProductImages;
public class ProductImagesMapper : IProductImagesMapper
{
    public IEnumerable<ProductsImages> changeProductId(IEnumerable<ProductsImages> oldList, int newId)
    {
        return oldList.Select(p => new ProductsImages()
        {
            imageURL = p.imageURL,
            productId = newId
        });
    }

    public  ProductsImages insertToModel(string insert, int productId)
    {
        return new ProductsImages()
        {
            productId = productId,
            imageURL = insert
        };
    }

    public IEnumerable<ProductsImages> insertToModelList(IEnumerable<string> insert, int productId)
    {
        return insert.Select(ff =>  insertToModel(ff, productId));
    }

    public ProductImagesReadDTO modelToRead(ProductsImages model)
    {
        return new ProductImagesReadDTO(model.productId, model.imageURL);
    }

    public IEnumerable<ProductImagesReadDTO> modelToReadList(IEnumerable<ProductsImages> modelList)
    {
      return  modelList.Select(pi => modelToRead(pi));
    }
}
