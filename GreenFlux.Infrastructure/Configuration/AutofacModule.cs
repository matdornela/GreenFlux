using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using GreenFlux.Infrastructure.DbContexts;
using Module = Autofac.Module;

namespace Infrastructure.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
                .Where(filePath => Path.GetFileName(filePath).StartsWith("GreenFlux") && !Path.GetFileName(filePath).Contains("Configuration"))
                .Select(Assembly.LoadFrom).ToArray();

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces();
        }
    }
}