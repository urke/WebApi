using SIENN.Models;
using SIENN.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIENN.DbAccess.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        IEnumerable<Product> Pagination(int page, int pageSize);
        IEnumerable<Product> DelivaryDateThisMonth();
        IEnumerable<Product> SearchProducts(int typeId,
                                            int categoryId,
                                            int productId,
                                            string description,
                                            int price,
                                            bool isAvailable,
                                            DateTime deliveryDate);

        void AddCategory(Product product, Category category);
        void IncludeProductCategorie(Product product);
        IEnumerable<Product> AvailableProductWithMoreCategories();
        IEnumerable<CategoryDTO> TopThreeCtegoriesMeanPrice();
    }
}
