using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.Infrastructures.Interfaces.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAsync();
    }
}
