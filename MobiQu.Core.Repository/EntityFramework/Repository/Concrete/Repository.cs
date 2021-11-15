using Microsoft.EntityFrameworkCore;
using MobiQu.Services.Core.Domain.DatabaseContext;
using MobiQu.Services.Core.Domain.Table;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Core.Persistence.EntityFramework.Repository.Concrete
{
    public class Repository<TTable> : IRepository<TTable> where TTable : class, IEntity, new()
    {
        private MobiQuContext _context;
        private DbSet<TTable> Table;
        public Repository()
        {
            _context = new MobiQuContext();
            Table = _context.Set<TTable>();
        }

        public async Task<TTable> FindAsync(Expression<Func<TTable, bool>> predicate) => await Table.FirstOrDefaultAsync(predicate);
        public TTable Find(Expression<Func<TTable, bool>> predicate) => Table.FirstOrDefault(predicate);
        public IEnumerable<TTable> GetList(Expression<Func<TTable, bool>> predicate = null) => predicate != null ? Table.Where(predicate).AsNoTracking() : Table.AsNoTracking();

        public IQueryable<TTable> Queryable() => Table.AsQueryable();

        public IQueryable<TTable> Queryable(Expression<Func<TTable, bool>> predicate = null) => predicate != null ? Table.Where(predicate).AsQueryable() : Table.AsQueryable();

        public async Task<int> DataCountAsync(Expression<Func<TTable, bool>> expression = null) => expression == null ? await Table.CountAsync() : await Table.CountAsync(expression);
        public int DataCount(Expression<Func<TTable, bool>> expression = null) => expression == null ? Table.Count() : Table.Count(expression);



    }
}
