using GenericRepository.Interfaces;
using GenericRepository.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GenericRepository.Services.InMemory
{
    public class GenericInMemoryAsyncRepository<TEntity> : IGenericAsyncRepository<TEntity>
    {
        private GenericInMemoryRepository<TEntity> _repository;

        public GenericInMemoryAsyncRepository(IEnumerable<TEntity> entities)
        {
            _repository = new GenericInMemoryRepository<TEntity>(entities);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetAsync([Required] DataModelOptions<TEntity> options, 
            CancellationToken cancellationToken = default) 
            => await Task.Run(() => _repository.Get(options), cancellationToken);

        /// <inheritdoc/>
        public async Task<TDestination> GetAsync<TDestination>([Required] ComplexDataModelOptions<TEntity, TDestination> options, 
            CancellationToken cancellationToken = default) 
            => await Task.Run(() => _repository.Get(options), cancellationToken);

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> ListAsync([Required] DataModelOptions<TEntity> options, 
            CancellationToken cancellationToken = default) 
            => await Task.Run(() => _repository.List(options), cancellationToken);

        /// <inheritdoc/>
        public async Task<IEnumerable<TDestination>> ListAsync<TDestination>([Required] ComplexDataModelOptions<TEntity, TDestination> options, 
            CancellationToken cancellationToken = default) 
            => await Task.Run(() => _repository.List(options), cancellationToken);

        /// <inheritdoc/>
        public async Task CreateAsync([Required] TEntity entity,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => _repository.Create(entity), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(
            [Required] Expression<Func<TEntity, bool>> searchClause,
            [Required] TEntity entity,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => _repository.Update(searchClause, entity), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(
            [Required] Expression<Func<TEntity, bool>> searchClause,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => _repository.Delete(searchClause), cancellationToken);
        }
    }
}
