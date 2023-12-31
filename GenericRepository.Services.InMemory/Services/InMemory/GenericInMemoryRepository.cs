﻿using GenericRepository.Interfaces;
using GenericRepository.Options;
using GenericRepository.Services.InMemory.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace GenericRepository.Services.InMemory
{
    public class GenericInMemoryRepository<TEntity>(IEnumerable<TEntity> entities) : IGenericRepository<TEntity>
    {
        private readonly EnumerableRepositoryHelper<TEntity> _helper = new(entities);

        /// <inheritdoc/>
        public TEntity Get([Required] DataModelOptions<TEntity> options)
        {
            return _helper.Get(options);
        }

        /// <inheritdoc/>
        public TDestination Get<TDestination>([Required] ComplexDataModelOptions<TEntity, TDestination> options)
        {
            return _helper.Get(options);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> List([Required] DataModelOptions<TEntity> options)
        {
            return _helper.List(options);
        }

        /// <inheritdoc/>
        public IEnumerable<TDestination> List<TDestination>([Required] ComplexDataModelOptions<TEntity, TDestination> options)
        {
            return _helper.List(options);
        }

        /// <inheritdoc/>
        public void Create(TEntity entity)
        {
            _helper.Create(entity);
        }

        /// <inheritdoc/>
        public void Update([Required] Expression<Func<TEntity, bool>> searchClause, TEntity entity)
        {
            _helper.Update(searchClause, entity);
        }

        /// <inheritdoc/>
        public void Delete([Required] Expression<Func<TEntity, bool>> searchClause)
        {
            _helper.Delete(searchClause);
        }
    }
}
