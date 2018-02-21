using SIENN.DAL;
using SIENN.DbAccess.IRepositories;
using SIENN.DbAccess.Repositories;
using SIENN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.DbAccess
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        SIENNDbContext _context;

        public UnitRepository(SIENNDbContext context) : base(context)
        {
            _context = context;
        }

        public SIENNDbContext _CONTEXT
        {
            get { return _context; }
        }
    }
}
