

using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product newProduct)
    {
        if (DataSource.ProductList.Exists(x => x?.Id == newProduct.Id))
            throw new Exception("There is already product with the same Id");
        else
            DataSource.ProductList.Add(newProduct);
        return newProduct.Id;
    }
    /// <summary>
    /// Request
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetById(int productId)
    {
        Product p = DataSource.ProductList.Find(x => x?.Id == productId) ?? throw new Exception("This product doesn't exist");
        return p;
    }
    /// <summary>
    /// UpDate
    /// </summary>
    /// <param name="productToUpDate"></param>
    /// <exception cref="Exception"></exception>
    public void UpDate(Product productToUpDate)
    {
        int ProductIndex = DataSource.ProductList.FindIndex(x => x?.Id == productToUpDate.Id);
        if (ProductIndex == -1)
            throw new Exception("This product doesn't exist");
        DataSource.ProductList[ProductIndex] = productToUpDate;
    }
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int productId)
    {
        int productIndex = DataSource.ProductList.FindIndex(x => x?.Id == productId);
        if (productIndex == -1)
            throw new Exception("This product doesn't exist");
        DataSource.ProductList.RemoveAt(productIndex);

    }
    /// <summary>
    /// GetAll
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll()
    {
        List<Product?> newList=new List<Product?>();
        DataSource.ProductList.ForEach(x=> newList.Add(x));
        return newList;
    }

}
