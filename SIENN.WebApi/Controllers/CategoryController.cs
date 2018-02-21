using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.IRepositories;
using SIENN.Models;


namespace SIENN.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public CategoryController(ICategoryRepository categoryRepository,
                                  IProductRepository productRepository
                                 )
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoryRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryRepository.Get(id);
        }


        [HttpPost("{description}")]
        public void Create(string description)
        {
            Category category = new Category();
            category.Description = description;
            _categoryRepository.Add(category);
        }


        [HttpPut("{id}/{description}")]
        public void Update(int id, string description)
        {
            Category oldCategory = _categoryRepository.Get(id);
            if (oldCategory != null)
            {
                oldCategory.Description = description;
                _categoryRepository.Update(oldCategory);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Category category = _categoryRepository.Get(id);
            if (category != null)
                _categoryRepository.Remove(category);
        }


        [HttpPost("/addProduct/{id}/{productId}")]
        public void addProduct(int id, int productId)
        {
            if (_categoryRepository.Get(id) == null
                || _productRepository.Get(productId) == null
               )
            { return; }
            Category category = _categoryRepository.Get(id);
            Product product = _productRepository.Get(productId);

            _categoryRepository.IncludeProductCategorie(category);
            _categoryRepository.AddProduct(category, product);
        }
    }
}
