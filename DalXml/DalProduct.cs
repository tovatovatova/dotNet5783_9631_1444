using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalProduct : IProduct
{
    readonly string s_products="products";

    public int Add(Product item)
    {
        List<Product?> lstProd =XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        bool x = lstProd.Any(prod => prod?.Id == item.Id);
        if (x)
            throw new DalIdAlreadyExistException(item.Id, "product");
        lstProd.Add(item);
        XMLTools.SaveListToXMLSerializer<DO.Product>(lstProd, s_products);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product? addProduct = lstProd.FirstOrDefault(prod => prod?.Id == id) ?? throw new DalIdDoNotExistException(id, "product");
        int productIndex = lstProd.FindIndex(x => x?.Id == id);
        lstProd.RemoveAt(productIndex);
        XMLTools.SaveListToXMLSerializer<DO.Product>(lstProd, s_products);
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        List<Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (filter != null)
            return lstProd.Where(item => filter(item));
        return lstProd.Select(item => item);
    }

    public Product GetById(int id)
    {
        List<Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        return lstProd.FirstOrDefault(prod => prod?.Id == id) ?? throw new DalIdDoNotExistException(id, "product");
    }

    public void Update(Product item)
    {
        List<Product?> lstProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product? addProduct = lstProd.FirstOrDefault(prod => prod?.Id == item.Id) ?? throw new DalIdDoNotExistException(item.Id, "product");
        int ProductIndex = lstProd.FindIndex(x => x?.Id == item.Id);
        lstProd[ProductIndex] = item;
        XMLTools.SaveListToXMLSerializer<DO.Product>(lstProd, s_products);
    }
}
