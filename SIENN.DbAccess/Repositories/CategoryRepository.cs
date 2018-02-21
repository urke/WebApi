using SIENN.DAL;
using SIENN.DbAccess.IRepositories;
using SIENN.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using PagedList;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SIENN.DbAccess.Repositories
{
    
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        
        SIENNDbContext _context;

        public CategoryRepository(SIENNDbContext context) : base(context)
        {
            _context = context;
        }

        public SIENNDbContext _CONTEXT
        {
            get { return _context; }
        }

        //include productCategory in entity
        public void IncludeProductCategorie(Category category)
        {

            _context.Categories.Where(x=>x.Id == category.Id)
                                                     .Include(c => c.ProductCategories)
                                                     .First();
        }

        /// Method Name: AddProduct
        /// <summary>
        /// Adding related product entity(many to many relationship)
        /// </summary>
        /// <param name="category"> Current category </param>
        /// <param name="product"> Related product </param>
        public void AddProduct(Category category, Product product)
        {
            if (_context.ProductCategories.Where(x => x.ProductId == product.Id 
                                                && x.CategoryId == category.Id
                                                )
                                          .FirstOrDefault() == null
               )
            {
                category.ProductCategories.Add(new ProductCategory { Product = product });
                _context.SaveChanges();
            }
        }
    }
}
