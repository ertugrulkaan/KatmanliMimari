﻿using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetAllByCategoryId(int id);
        List<Product> GetAllByUnitPrice(decimal min, decimal max);
        Product Get(Product product);
        void Add(Product product);
        void Delete(Product product);
        List<ProductDetailDto> GetProductDetails();


    }
}
