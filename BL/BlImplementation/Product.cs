﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation
{
    internal class Product : IProduct
    {
        public void AddProduct(BO.Product newProduct)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetCatalog()
        {
            throw new NotImplementedException();
        }

        public ProductItem GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public BO.Product GetProductDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Product> GetProductList()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(BO.Product product)
        {
            throw new NotImplementedException();
        }
    }
}
