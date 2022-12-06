

using DO;
using DalApi;
namespace Dal;

public class DalProduct : IProduct
{
    /// <summary>
    /// add the new given  product to list of products
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns>newProduct.Id</returns>
    /// <exception cref="Exception">throw exeption when there is already product with the same id</exception>
    public int Add(Product newProduct)
    {

       bool x= DataSource.ProductList.Any(prod => prod.Value.Id == newProduct.Id);
            if(x)
              throw new DalIdAlreadyExistException(newProduct.Id, "product");
        DataSource.ProductList.Add(newProduct);
        return newProduct.Id;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>product</returns>
    /// <exception cref="Exception">trow exeption when the product doesnt exist</exception>
    public Product GetById(int productId)
    {
        return DataSource.ProductList.FirstOrDefault(prod => prod.Value.Id == productId) ?? throw new DalIdDoNotExistException(productId, "product");
    }
    /// <summary>
    /// UpDate the specific product according to the given product
    /// </summary>
    /// <param name="productToUpDate"></param>
    /// <exception cref="Exception">throw exeption when product doesnt exist</exception>
    public void Update(Product productToUpDate)
    {
        Product? addProduct = DataSource.ProductList.FirstOrDefault(prod => prod.Value.Id == productToUpDate.Id) ?? throw new DalIdDoNotExistException(productToUpDate.Id, "product");
        int ProductIndex = DataSource.ProductList.FindIndex(x => x?.Id == productToUpDate.Id);
        DataSource.ProductList[ProductIndex] = productToUpDate;
    }
    /// <summary>
    /// Delete product according to the fiven id
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="Exception">throw exeption when product doesnt exist</exception>
    public void Delete(int productId)
    {
        Product? addProduct = DataSource.ProductList.FirstOrDefault(prod => prod.Value.Id == productId) ?? throw new DalIdDoNotExistException(productId, "product");
        int productIndex = DataSource.ProductList.FindIndex(x => x?.Id == productId);
        DataSource.ProductList.RemoveAt(productIndex);
    }
    /// <summary>
    /// add all the products to a new list and return the list
    /// </summary>
    /// <returns>newList</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.ProductList.Where(item => filter(item));
        return DataSource.ProductList.Select(item => item);
    }
}
