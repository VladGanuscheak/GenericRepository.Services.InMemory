using GenericRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GenericRepository.Services.InMemory.Host
{
    public static class GenericRepositoryInMemoryInstaller
    {
        public static IServiceCollection AddGenericRepositoryInMemoryInstaller(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericInMemoryRepository<>));
            services.AddTransient(typeof(IGenericAsyncRepository<>), typeof(GenericInMemoryAsyncRepository<>));

            return services;
        }
    }
}
