

using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// add the new given  product to list of products
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns>newProduct.Id</returns>
    /// <exception cref="Exception">throw exeption when there is already product with the same id</exception>
    public int Add(Product newProduct)
    {
        if (DataSource.ProductList.Exists(x => x?.Id == newProduct.Id))
            throw new Exception("There is already product with the same Id");
        else
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
        Product product = DataSource.ProductList.Find(x => x?.Id == productId) ?? throw new Exception("This product doesn't exist");
        return product;
    }
    /// <summary>
    /// UpDate the specific product according to the given product
    /// </summary>
    /// <param name="productToUpDate"></param>
    /// <exception cref="Exception">throw exeption when product doesnt exist</exception>
    public void UpDate(Product productToUpDate)
    {
        int ProductIndex = DataSource.ProductList.FindIndex(x => x?.Id == productToUpDate.Id);
        if (ProductIndex == -1)
            throw new Exception("This product doesn't exist");
        DataSource.ProductList[ProductIndex] = productToUpDate;
    }
    /// <summary>
    /// Delete product according to the fiven id
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="Exception">throw exeption when product doesnt exist</exception>
    public void Delete(int productId)
    {
        int productIndex = DataSource.ProductList.FindIndex(x => x?.Id == productId);
        if (productIndex == -1)
            throw new Exception("This product doesn't exist");
        DataSource.ProductList.RemoveAt(productIndex);
    }
    /// <summary>
    /// add all the products to a new list and return the list
    /// </summary>
    /// <returns>newList</returns>
    public IEnumerable<Product?> GetAll()
    {
        List<Product?> newList=new List<Product?>();
        DataSource.ProductList.ForEach(x=> newList.Add(x));
        return newList;
    }

}
