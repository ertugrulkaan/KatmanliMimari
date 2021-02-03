using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //GC nin bu nesneyi isi bitince silmesi icin yaptik
            using (NorthwindContext context=new NorthwindContext())
            {
                // 1 gonderilen nesnenin referansini al
                // 2 yontemi belirt
                // 3 ekle.
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // 1 gonderilen nesnenin referansini al
                // 2 yontemi belirt
                // 3 ekle.
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // ?  dbsetteki product a yerles butun verileri bana gonder : degil ise filtreyi uygula
                // parametre olarak bir lambda olacak filtre ne olursa o olacak
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // 1 gonderilen nesnenin referansini al
                // 2 yontemi belirt
                // 3 ekle.
                var updateddEntity = context.Entry(entity);
                updateddEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
