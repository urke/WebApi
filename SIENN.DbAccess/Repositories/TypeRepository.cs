using SIENN.DAL;
using SIENN.DbAccess.IRepositories;
using SIENN.DbAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Type = SIENN.Models.Type;


namespace SIENN.DbAccess
{
    public class TypeRepository : GenericRepository<Type>, ITypeRepository
    {
        SIENNDbContext _context;

        public TypeRepository(SIENNDbContext context) : base(context)
        {
            _context = context;
        }

        public SIENNDbContext _CONTEXT
        {
            get { return _context; }
        }
    }
}
