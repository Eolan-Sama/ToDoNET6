﻿using NLayer.Core.Repo;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NLayer.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
            private readonly IGenericRepository<T> _repository;
            private readonly IUnitOfWork _unitOfWork;

            public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<T> GetByIdAsync(int id)
            {
                return await _repository.GetByIdAsync(id);
            }


            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _repository.GetAll().ToListAsync();
            }

            public IQueryable<T> Where(Expression<Func<T, bool>> expression)
            {
                return _repository.Where(expression);
            }

            public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
            {
                return await _repository.AnyAsync(expression);
            }

            public async Task<T> AddAsync(T entity)
            {
                await _repository.AddAsync(entity);
                await _unitOfWork.CommitAsync();
                return entity;
            }


            public async Task UpdateAsync(T entity)
            {
                _repository.Update(entity);
                await _unitOfWork.CommitAsync();
            }

            public async Task RemoveAsync(T entity)
            {
                _repository.Remove(entity);
                await _unitOfWork.CommitAsync();
            }

        }
    }

