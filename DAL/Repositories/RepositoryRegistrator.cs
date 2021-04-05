using Microsoft.Extensions.DependencyInjection;

namespace DAL.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<UsersRepository>()
            .AddTransient<QuestionsRepository>()
            .AddTransient<ExamsRepository>()
            .AddTransient<StatisticsRepository>()
            ;
    }
}
