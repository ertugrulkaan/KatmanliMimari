using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Subat6();
            //Subat3();
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();
            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " - " + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void Subat6()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void Subat3()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var product2 in productManager.GetAllByCategoryId(1).Data)
            {
                Console.WriteLine(product2.ProductName);
            }
            foreach (var product2 in productManager.GetAllByUnitPrice(5, 10).Data)
            {
                Console.WriteLine(product2.ProductName);
            }
            Product product = productManager.Get(new Product() { ProductName = "Sirop d'érable" }).Data;
            Console.WriteLine(product.ProductId + " - " + product.CategoryId + " - " + product.ProductName + " - " + product.UnitPrice);
            Product product3 = new Product() { CategoryId = 2, ProductName = "test", UnitPrice = 333, UnitsInStock = 5 };
            //productManager.Add(product3);
            Console.WriteLine(productManager.Get(product3).Data.ProductId);
            //productManager.Delete(productManager.Get(product3));
        }
    }
}
