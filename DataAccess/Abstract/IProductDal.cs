﻿using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        // BU TABLOYA OZEL METHODLAR BURDA YAPILIR VE EFPRODUCTDAL IN ICERISINDE DOLDURULUR.
    }
}
