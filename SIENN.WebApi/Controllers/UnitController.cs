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
    [Route("api/Unit")]
    public class UnitController : Controller
    {
        private IUnitRepository _unitRepository;

        public UnitController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public IEnumerable<Unit> Get()
        {
            return _unitRepository.GetAll();
        }


        [HttpGet("{id}")]
        public Unit Get(int id)
        {
            Unit unit = _unitRepository.Get(id);
            return _unitRepository.Get(id);
        }
        

        [HttpPost("{description}")]
        public void Create(string description)
        {
            Unit unit = new Unit();
            unit.Description = description;
            _unitRepository.Add(unit);
        }
        

        [HttpPut("{id}/{description}")]
        public void Update(int id, string description)
        {
            Unit oldUnit = _unitRepository.Get(id);
            if (oldUnit != null)
            {
                oldUnit.Description = description;
                _unitRepository.Update(oldUnit);
            }
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Unit unit = _unitRepository.Get(id);
            if (unit != null)
                _unitRepository.Remove(unit);
        }
    }
}
