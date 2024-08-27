using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Repositories;
using StackExchange.Redis;

namespace Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        const string dbConnection = "Server=localhost;Database=master;Trusted_Connection=True;Encrypt=False;";


        services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(dbConnection));
        services.AddScoped<ITodoDetailRepository, TodoDetailRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}