using GenericRepository.Enums;
using GenericRepository.Interfaces;
using GenericRepository.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace GenericRepository.Services.InMemory.Helpers
{
    public class EnumerableRepositoryHelper<TEntity> : IGenericRepository<TEntity>
    {
        private IEnumerable<TEntity> _entities;

        public EnumerableRepositoryHelper(IEnumerable<TEntity> entities)
        {
            _entities = entities;
        }

        private IEnumerable<TType> SortEntities<TType>(IEnumerable<TType> entities, string sortingFieldName, SortingOrder sortingOrder)
        {
            if (!string.IsNullOrEmpty(sortingFieldName) && !string.IsNullOrWhiteSpace(sortingFieldName))
            {
                var property = typeof(TType)
                    .GetProperty(sortingFieldName);

                if (sortingFieldName == null)
                {
                    throw new ArgumentException();
                }

                switch (sortingOrder)
                {
                    case SortingOrder.Asc:
                        entities = entities.OrderBy(x => (dynamic)property.GetValue(x));
                        break;
                    case SortingOrder.Desc:
                        entities = entities.OrderByDescending(x => (dynamic)property.GetValue(x));
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            return entities;
        }

        public IEnumerable<TEntity> List([Required] DataModelOptions<TEntity> options)
        {
            var result = _entities;
            if (options.EntitySearchClause != null)
            {
                result = result.Where(options.EntitySearchClause.Compile());
            }

            result = SortEntities(result, options.SortingFieldName, options.SortingOrder);

            return result;
        }

        public IEnumerable<TDestination> List<TDestination>([Required] ComplexDataModelOptions<TEntity, TDestination> options)
        {
            var entities = _entities;
            if (options.EntitySearchClause != null)
            {
                entities = entities.Where(options.EntitySearchClause.Compile());
            }

            var result = entities.Select(options.Projection.Compile());

            if (options.ProjectionSearchClause != null)
            {
                result = result.Where(options.ProjectionSearchClause.Compile());
            }

            result = SortEntities(result, options.SortingFieldName, options.SortingOrder);

            return result;
        }

        public TEntity Get([Required] DataModelOptions<TEntity> options) 
            => List(options).FirstOrDefault();

        public TDestination Get<TDestination>([Required] ComplexDataModelOptions<TEntity, TDestination> options)
            => List(options).FirstOrDefault();

        public void Create(TEntity entity)
        {
            _entities = _entities.Append(entity);
        }

        public void Update(Expression<Func<TEntity, bool>> searchClause, TEntity entity)
        {
            var items = _entities.Except(_entities.Where(searchClause.Compile()));
            items = items.Append(entity);
            _entities = items;
        }

        public void Delete(Expression<Func<TEntity, bool>> searchClause)
        {
            _entities = _entities.Except(_entities.Where(searchClause.Compile()));
        }
    }
}
