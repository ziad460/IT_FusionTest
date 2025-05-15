using ITFusionTask.Data.UnityOfWrok;
using ITFusionTask.Reposatories.EmployeeReposatory;
using ITFusionTask.Services.EmployeeService;
using Microsoft.EntityFrameworkCore;


namespace ITFusionTask
{
    public static class ServiceCollectionExtensions
    {
        public static void ServiceLibrary(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        public static void AddUnitOfWork<TContext>(this IServiceCollection services, IConfiguration Configuration)
            where TContext : DbContext
        {
            services.AddScoped<IUnityOfWork<TContext>, UnityOfWork<TContext>>();
            services.AddScoped<IUnityOfWork, UnityOfWork<TContext>>();
        }

        public static void AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services, IConfiguration Configuration)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IUnityOfWork<TContext1>, UnityOfWork<TContext1>>();
            services.AddScoped<IUnityOfWork<TContext2>, UnityOfWork<TContext2>>();
        }

        public static void AddUnitOfWork<TContext1, TContext2, TContext3>(this IServiceCollection services, IConfiguration Configuration)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IUnityOfWork<TContext1>, UnityOfWork<TContext1>>();
            services.AddScoped<IUnityOfWork<TContext2>, UnityOfWork<TContext2>>();
            services.AddScoped<IUnityOfWork<TContext3>, UnityOfWork<TContext3>>();
        }
    }
}
