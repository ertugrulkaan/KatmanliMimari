﻿using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        //Global Variables
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> 
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitsInStock=15,UnitPrice=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitsInStock=3,UnitPrice=500},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitsInStock=20,UnitPrice=1500},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitsInStock=65,UnitPrice=150},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitsInStock=1,UnitPrice=85},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // Bu sekilde calismaz cunku referans adresleri farkli bunu gonderilen productin id sini o listede aratarak silmek lazim
            //_products.Remove(product);

            // LANGUAGE INTEGRATED QUERY = LINQ
            //// linq bilmeden boyle yapilir.
            //Product productToDelete = null;
            //foreach (var p in _products)
            //{
            //    if (p.ProductId==product.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}

            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
            if (productToDelete!=null)
            {
                _products.Remove(productToDelete);
            }


        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {

            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            //Gonderdigim urunid ye sahip urunu listede bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            // referans tip oldugundan bunlari yapmamiz yeterli.
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
