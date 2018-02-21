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
    [Route("api/Type")]
    public class TypeController : Controller
    {
        private ITypeRepository _typeRepository;

        public TypeController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }


        [HttpGet]
        public IEnumerable<Models.Type> Get()
        {
            return _typeRepository.GetAll();
        }


        [HttpGet("{id}")]
        public Models.Type Get(int id)
        {
            Models.Type tip = _typeRepository.Get(id);
            return _typeRepository.Get(id);
        }


        [HttpPost("{description}")]
        public void Create(string description)
        {
            Models.Type type = new Models.Type();
            type.Description = description;
            _typeRepository.Add(type);
        }


        [HttpPut("{id}/{description}")]
        public void Update(int id, string description)
        {
            Models.Type oldtype = _typeRepository.Get(id);
            if (oldtype != null)
            {
                oldtype.Description = description;
                _typeRepository.Update(oldtype);
            }
        }
        

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Models.Type type = _typeRepository.Get(id);
            if (type != null)
                _typeRepository.Remove(type);
        }
    }
}
