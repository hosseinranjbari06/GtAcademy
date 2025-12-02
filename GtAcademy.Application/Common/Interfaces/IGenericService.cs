using GtAcademy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IGenericService<T> where T : BaseDomain
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<T?> GetByIdAsync(object id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
