using Microsoft.Extensions.DependencyInjection;
using MyBookshelf.Core.Interfaces.Repositories;
using MyBookshelf.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookshelf.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IUserBookRepository, UserBookRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IStatusHistoryRepository, StatusHistoryRepository>();

            return services;
        }
    }
}
