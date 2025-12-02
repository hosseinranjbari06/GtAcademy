using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Common.Persistence
{
    public class GenericService<T> : IGenericService<T> where T : BaseDomain
    {
        private readonly GtAcademyDbContext _db;

        public GenericService(GtAcademyDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }
    }
}
