
using Microsoft.EntityFrameworkCore;
using SIENN.DAL;
using SIENN.DbAccess.IRepositories;
using SIENN.DbAccess.Repositories;
using SIENN.Models;
using SIENN.Models.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIENN.DbAccess
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        SIENNDbContext _context;

        public ProductRepository(SIENNDbContext context) : base(context)
        {
            _context = context;
        }

        /// Method Name: Paganation
        /// <summary>
        /// Paging based on wanted page number and page size
        /// </summary>
        /// <param name="page"> Page number </param>
        /// <param name="pageSize"> Number of object a page </param>
        /// <returns> List of object on selected page with selected number of objects
        public IEnumerable<Product> Pagination(int page, int pageSize)
        {
            DbSet<Product> products = _context.Products;

            int numberOfObjectsPerPage = pageSize;
            var resultPageList = (from p in products
                                   where p.IsAvailable == true
                                   select p
                                 )
                                 .Skip(numberOfObjectsPerPage * page)
                                 .Take(numberOfObjectsPerPage);

            return resultPageList;
        }
        
        ///Method Name : AvailableProductWithMoreCategories
        /// <summary>
        /// Query for selecting products with more then one category
        /// </summary>
        /// <returns> Matched products </returns>
        public IEnumerable<Product> AvailableProductWithMoreCategories()

        {
            var result = _context.Categories
                         .Join(_context.ProductCategories,
                               c => c.Id, pc => pc.CategoryId, (c, pc) => pc
                              )
                          .Join(_context.Products,
                                pc => pc.ProductId, p => p.Id, (pc, p) => p
                                )
                          .Where(p => p.IsAvailable == true)
                          .GroupBy(p => p.Id)
                          .Where(group => group.Count() > 1);


            IEnumerable<Product> matchedProducts = result
                                                   .SelectMany(p => p)
                                                   .Distinct();
            return matchedProducts;
        }

        /// Method Name : TopThreeCtegoriesMeanPrice
        /// <summary>
        /// Query for selecting top 3 categories with available products
        /// and mean price of products
        /// </summary>
        /// <returns> Matched categories </returns>
        public IEnumerable<CategoryDTO> TopThreeCtegoriesMeanPrice()
        {

            var result = (from p in _context.Products
                            join pc in _context.ProductCategories on p.Id equals pc.ProductId
                            join c in _context.Categories on pc.CategoryId equals c.Id
                            where p.IsAvailable == true
                            group p by new { c.Description, c.Id } into gr
                            select new { Category = gr.Key, MeanPrice = gr.Average(x => x.Price), NumberOfAvaillableProducts = gr.Count(x => x.IsAvailable) } 
                            into selection
                            orderby selection.MeanPrice descending 
                            select selection)
                            .Take(3);


            List<CategoryDTO> categoryDTOList = new List<CategoryDTO>();

            foreach(var category in result)
            {
                categoryDTOList.Add(new CategoryDTO
                {
                    Id = category.Category.Id,
                    Description = category.Category.Description,
                    MeanPrice = category.MeanPrice,
                    NumberOfAsignedProducts = category.NumberOfAvaillableProducts
                }
                                   );
            }
            return categoryDTOList;
        }

        ///Method Name : DelivaryDateThisMonth
        /// <summary>
        /// Deliver all producted with delivery date property value of this month
        /// </summary>
        /// <returns> List of matched products </returns>
        public IEnumerable<Product> DelivaryDateThisMonth()
        {
            DateTime today = new DateTime();

            var products = _context.Products.Where(p => p.DelivaryDate.Month
                                                        .Equals(today.Month));

            return products;
        }

        /// Method Name : SearchProducts
        /// <summary>
        /// Search wanted product based on entered parameters
        /// </summary>
        /// <param name="typeId"> id of type entity </param>
        /// <param name="categoryId"> id of category entity </param>
        /// <param name="productId"> id of product entity </param>
        /// <param name="description"> product descripton </param>
        /// <param name="price"> product price </param>
        /// <param name="isAvailable"> is product available </param>
        /// <param name="deliveryDate"> product deliveryDate </param>
        /// <returns> List of matched products </returns>
        public IEnumerable<Product> SearchProducts(int typeId,
                                                   int categoryId,
                                                   int productId,
                                                   string description,
                                                   int price,
                                                   bool isAvailable,
                                                   DateTime deliveryDate
                                                  )
        {
            var filteredProducts = new List<Product>();

            foreach (Product product in _context.Products)
            {
                if (typeId != null && product.TypeId != typeId)
                    continue;

                foreach (ProductCategory projectCategories in product.ProductCategories)
                {
                    if (categoryId != null && projectCategories.CategoryId != categoryId)
                        continue;
                }

                if (productId != null && product.Id != productId)
                    continue;

                if (description != null && product.Description != description)
                    continue;

                if (price != null && product.Price != price)
                    continue;

                if (isAvailable != null && product.IsAvailable != isAvailable)
                    continue;

                if (deliveryDate != null && product.DelivaryDate.Date.ToString() !=
                                                    deliveryDate.Date.ToString())
                    continue;

                filteredProducts.Add(product);
            }

            return filteredProducts;
        }

        /// Method Name: IncludeProductCategorie
        /// <summary>
        /// Include all related categories
        /// </summary>
        /// <param name="product"></param>
        public void IncludeProductCategorie(Product product)
        {

            _context.Products.Where(x => x.Id == product.Id)
                                                     .Include(c => c.ProductCategories)
                                                     .First();
        }

        /// Method Name: AddCategory
        /// <summary>
        /// Adding related category entity(many to many relationship)
        /// </summary>
        /// <param name="product"> Current product </param>
        /// <param name="category"> Related category </param>

        public void AddCategory(Product product,Category category)
        {
            if(_context.ProductCategories.Where(x=>x.ProductId == product.Id 
                                                && x.CategoryId==category.Id
                                                )
                                         .FirstOrDefault() == null
              )
            {
                product.ProductCategories.Add(new ProductCategory { Category = category });
                _context.SaveChanges();
            }
        }        
    }
}
