using Ss.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Service.Interfaces
{
    public interface IService<TModel, TView> 
        where TModel : IEntity, new() 
        where TView : IViewEntity, new()
    {
        IEnumerable<TView> Get(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, string includeProperties = "");

        TModel GetSingle(Expression<Func<TModel, bool>> predicate);

        TView GetByID(object id);

        void Insert(TModel entity);

        void Delete(object id);

        void Delete(TModel entityToDelete);

        void Update(TModel entityToUpdate);

        void Save();
    }
}
