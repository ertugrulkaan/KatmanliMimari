using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        // Business katmani baska bir classi newlemez

        // tek dondurenler get 
        // liste dodnurenler getall

        IProductDal _productDal;
        //baska bir tabloyu alakadar eden bir kural gelecek ise o kurala ait nesnenin servisini cagirmaliyiz
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //[SecuredOperation("admin")]
        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfProductExits(product.ProductName), CheckIfCategoryCountLimitExceeded());
            //yeni bir is kurali gelir ise uste virgul ile eklemek yeterli
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> Get(Product product)
        {
            // dataresult da true gondermek yerine success kullandik
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductName == product.ProductName), "Urunler Listelendi.");
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
            //Is kodlari buraya gelecek
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), Messages.ProductsListed);
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId), Messages.ProductListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductsListed);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductUpdated);
            }
            return new ErrorResult(Messages.ProductCannotUpdated);

        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductExits(string productName)
        {
            bool result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductExitsError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryCountLimitExceeded()
        {
            int result = _categoryService.GetAll().Data.Count;
            if (result>15)
            {
                return new ErrorResult(Messages.CategoryCountLimitExceeded);
            }
            return new SuccessResult();
        }

        //[TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Add(product);
                    if (product.UnitPrice < 10)
                    {
                        throw new Exception("");
                    }
                    Add(product);
                    scope.Complete();

                }
                catch (Exception)
                {
                    scope.Dispose();
                }
            }
            return null;
        }
    }
}
