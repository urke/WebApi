using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using SIENN.DbAccess.IRepositories;
using SIENN.Models;
using System.Net.Http;
using SIENN.Models.DTO;

namespace SIENN.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private IUnitRepository _unitRepository;
        private ICategoryRepository _categoryRepository;
        private ITypeRepository _typeRepository;


        public ProductController(IProductRepository productRepository,
                                 IUnitRepository unitRepository,
                                 ICategoryRepository categoryRepository,
                                 ITypeRepository typeRepository)
        {
            _productRepository = productRepository;
            _unitRepository = unitRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
        }


        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepository.GetAll();
        }


        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productRepository.Get(id);
            
        }


        [HttpPost("{description}/{price}/{isAvailable}/{deliveryDate}/{unitId}")]
        public void Create(string description,
                           int price,
                           bool isAvailable,
                           DateTime deliveryDate,
                           int? typeId,
                           int unitId
                           )
        {
            Product product = new Product();
            product.Description = description;
            product.Price = price;
            product.IsAvailable = isAvailable;
            product.DelivaryDate = deliveryDate;
            product.TypeId = typeId;
            product.UnitID = unitId;

            Unit unit = _unitRepository.Get(unitId);
            //if there is not existing unit id, do not create
            if (unit == null)
                return;
            product.Unit = unit;
            //check if type also exist, if not do not create
            if (typeId.HasValue && _typeRepository.Get(typeId.Value) != null)
                product.Type = _typeRepository.Get(typeId.Value);

            _productRepository.Add(product);
        }


        [HttpPut("{id}/{description}/{price}/{isAvailable}/{deliveryDate}/{unitId}")]
        public void Update(int id,
                           string description,
                           int price,
                           bool isAvailable,
                           DateTime deliveryDate,
                           int? typeId,
                           int unitId
                          )
        {
            Product oldProduct = _productRepository.Get(id);
            if (oldProduct != null)
            {
                oldProduct.Description = description;
                oldProduct.Price = price;
                oldProduct.IsAvailable = isAvailable;
                oldProduct.DelivaryDate = deliveryDate;
                oldProduct.TypeId = typeId;
                oldProduct.UnitID = unitId;

                Unit unit = _unitRepository.Get(unitId);
                //if there is not existing unit id, do not update
                if (unit == null)
                    return;
                oldProduct.Unit = unit;
                //check if type also exist, if not do not update
                if (typeId.HasValue && _typeRepository.Get(typeId.Value) != null)
                    oldProduct.Type = _typeRepository.Get(typeId.Value);

                _productRepository.Update(oldProduct);
            }
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Product product = _productRepository.Get(id);
            if (product != null)
                _productRepository.Remove(product);
        }


        [HttpGet("pagination/{page}/{pageSize}")]
        public IEnumerable<Product> DataPaging(int page, int pageSize)
        {
            return _productRepository.Pagination(page, pageSize);
        }

        /*
        [HttpGet("byType/{typeId}")]
        public IEnumerable<Product> GetFilteredByType(int typeId)
        {
            Models.Type type = new Models.Type();
            type.Id = typeId;
            return _productRepository.FilteringByType(type);
        }


        [HttpGet("/byCategory/{categoryId}")]
        public IEnumerable<Product> GetFilteredByCategory(int categoryId)
        {
            Category category = new Category();
            category.Id = categoryId;
            return _productRepository.FilteringByCategory(category);
        }
        */

        [HttpGet("/byDeliveryMonth")]
        public IEnumerable<Product> GetDelivaryDateThisMonth()
        {
            return _productRepository.DelivaryDateThisMonth();
        }


        [HttpGet("/productDetailsFormatted")]
        public IEnumerable<ProductDTO> GetProductDetailsFormatted()
        {
            var products = _productRepository.GetAll();
            var dtoProducts = new List<ProductDTO>();
            foreach (Product product in products)
            {
                dtoProducts.Add(new ProductDTO(product));
            }
            return dtoProducts;
        }


        [HttpPost("/searchProduct")]
        public IEnumerable<Product> SearchProducts([FromBody] SearchDTO searchDto)
        {
            return _productRepository.SearchProducts(searchDto.SearchByTypeId,
                                                     searchDto.SearchByCategory,
                                                     searchDto.SearchByProductId,
                                                     searchDto.SearchByDescription,
                                                     searchDto.SearchByPrice,
                                                     searchDto.SearchByIsAvailable,
                                                     searchDto.SearchByDelivaryDate
                                                     );
        }


        [HttpPost("/addCategory/{id}/{categoryId}")]
        public void AddCategory(int id, int categoryId)
        {
            if (_productRepository.Get(id)==null
                || _categoryRepository.Get(categoryId)==null
               )
            { return; }
            Product product = _productRepository.Get(id);
            Category category = _categoryRepository.Get(categoryId);

            _productRepository.IncludeProductCategorie(product);
            _productRepository.AddCategory(product,category);
        }

        [HttpGet("/availableProductWithMoreCategories")]
        public IEnumerable<Product> GetProductWithMoreCategories()
        {
            return _productRepository.AvailableProductWithMoreCategories();   
        }

        [HttpGet("/TopThreeCtegoriesMeanPrice")]
        public IEnumerable<CategoryDTO> GetTopThreeCtegoriesMeanPrice()
        {
            
            return _productRepository.TopThreeCtegoriesMeanPrice();
         
        }
    }
}
