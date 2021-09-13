using Autofac;
using GreenFlux.API.DbContexts;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace API.Infrastructure.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
                .Where(filePath => Path.GetFileName(filePath).StartsWith("API") && !Path.GetFileName(filePath).Contains("Configuration"))
                .Select(Assembly.LoadFrom).ToArray();

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces();
        }
    }
}