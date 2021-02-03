using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        // Business katmani baska bir classi newlemez

        // tek dondurenler get 
        // liste dodnurenler getall

        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public Product Get(Product product)
        {
            return _productDal.Get(p=> p.ProductName==product.ProductName);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
            //Is kodlari buraya gelecek
        }

        public List<Product> GetAllByCategoryId(int id)
        {
            return _productDal.GetAll(p => p.ProductId == id);
        }

        public List<Product> GetAllByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice<=max);
        }
    }
}
