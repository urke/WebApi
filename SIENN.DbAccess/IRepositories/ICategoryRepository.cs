using SIENN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.DbAccess.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        void AddProduct(Category category, Product product);
        void IncludeProductCategorie(Category category);
    }
}
