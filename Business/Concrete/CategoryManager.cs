using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        // DATAACCESS DEN ILGILI NESNEYI ALIP CONSTRUCTOR INJECTION ILE EKLE
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        // METHOD NEYE GIDECEK NE DONDURECEK
        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(),"basarili");
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }
        //public IDataResult<List<Category>> GetList()
        //{
        //    return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());
        //}
    }
}
