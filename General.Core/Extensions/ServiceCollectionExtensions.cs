using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAssembly(this IServiceCollection services,string assemblyName
            , ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            //var assmbly = RunTimeHelper.GetAssemblyByName(assemblyName);
            //var types = assmbly.GetTypes();
            //var allClasses = types.Where(o => o.IsClass && !o.IsAbstract && !o.IsGenericType);
            //foreach (var item in allClasses)
            //{
            //    services.AddScoped(item.GetInterfaces()[0], item);
            //}
            if (services == null)
                throw new ArgumentNullException(nameof(services) + "为空");

            if (String.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName) + "为空");

            var assembly = RunTimeHelper.GetAssemblyByName(assemblyName);

            if (assembly == null)
                throw new DllNotFoundException(nameof(assembly) + ".dll不存在");

            var types = assembly.GetTypes();
            var list = types.Where(o => o.IsClass && !o.IsAbstract && !o.IsGenericType).ToList();
            if (list == null && !list.Any())
                return;
            foreach (var type in list)
            {
                var interfacesList = type.GetInterfaces();
                if (interfacesList == null || !interfacesList.Any())
                    continue;
                var inter = interfacesList.First();
                switch (serviceLifetime)
                {
                    case ServiceLifetime.Scoped:
                        services.AddScoped(inter, type);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(inter, type);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(inter, type);
                        break;
                }
            }
        }
    }
}
