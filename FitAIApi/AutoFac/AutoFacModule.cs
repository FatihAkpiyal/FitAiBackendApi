using Autofac;
using FitAIAPI.Application.Interfaces;
using FitAIAPI.Application.Mappers;
using FitAIAPI.Application.Services;
using FitAIAPI.Domain.Repositories;
using FitAIAPI.Infrastructure.Data.Context;
using FitAIAPI.Infrastructure.Repositories;
using System.Reflection;
using Module = Autofac.Module;

namespace FitAIApi.AutoFac
{
    public class AutoFacModule: Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            var apiAssembly = Assembly.GetExecutingAssembly();

            var repoAssembly = Assembly.GetAssembly(typeof(FitAIDbContext));

            var serviceAssembly = Assembly.GetAssembly(typeof(MapperProfile));

            containerBuilder.RegisterAssemblyTypes(apiAssembly, repoAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(BaseService<,>)).As(typeof(IBaseService<,>)).InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(serviceAssembly, apiAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .SingleInstance();
        }
    }
}
