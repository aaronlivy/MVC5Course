using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => false == p.IsDelete && p.ProductId == id);
        }

        public override IQueryable<Product> All()
        {
            return base.All().Where(o =>  false == o.IsDelete && o.Stock < 500);
        }

        public IQueryable<Product> All(bool ShowAll)
        {
            return (ShowAll ? base.All() : this.All());
        }

        public override void Delete(Product entity)
        {
            entity.IsDelete = true;
        }

    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}