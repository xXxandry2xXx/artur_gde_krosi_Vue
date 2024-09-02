using artur_gde_krosi_Vue.Server.Contracts.Repositories;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace artur_gde_krosi_Vue.Server.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationIdentityContext ApplicationContext;
        public RepositoryBase(ApplicationIdentityContext _applicationIdentityContext)
        {
            ApplicationContext = _applicationIdentityContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
              ApplicationContext.Set<T>().AsNoTracking() :
              ApplicationContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
              ApplicationContext.Set<T>().Where(expression).AsNoTracking() :
              ApplicationContext.Set<T>().Where(expression);

        public void Create(T entity) => ApplicationContext.Set<T>().Add(entity);

        public void Update(T entity) => ApplicationContext.Set<T>().Update(entity);

        public void Delete(T entity) => ApplicationContext.Set<T>().Remove(entity);
    }
}
