using MobiQu.Services.Core.Domain.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction
{
    public interface IRepository<TTable> where TTable : class, IEntity, new()
    {
        IEnumerable<TTable> GetList(Expression<Func<TTable, bool>> predicate = null);
        IQueryable<TTable>  Queryable(Expression<Func<TTable, bool>> predicate = null);

        Task<TTable> FindAsync(Expression<Func<TTable, bool>> predicate = null);
        Task<int> DataCountAsync(Expression<Func<TTable, bool>> expression = null);

        int DataCount(Expression<Func<TTable, bool>> expression = null);



    }
}
