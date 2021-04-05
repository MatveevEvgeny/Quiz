using DAL.Context;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Quiz.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<QuizDB>(opt =>
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case null: throw new InvalidOperationException("Не определен тип БД");
                    default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

                    case "MSSQL":
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                }
            })
            .AddTransient<DbInitializer>()
            .AddRepositoriesInDB()
            ;
    }
}
