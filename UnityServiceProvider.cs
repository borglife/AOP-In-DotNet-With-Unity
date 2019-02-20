namespace NorDev.Aop.Web
{
    using System;
    using System.Configuration;
    using Microsoft.Practices.Unity.Configuration;
    using Unity;
    using Unity.Exceptions;

    public class UnityServiceProvider : IServiceProvider
    {
        public IUnityContainer Container { get; }

        public UnityServiceProvider(string filename = @"unity.config")
        {
            filename = System.IO.Path.Combine(Startup.HostingEnvironment.ContentRootPath, filename);
            this.Container = new UnityContainer();
            ConfigureContainer(this.Container, filename);
        }

        private static void ConfigureContainer(IUnityContainer container, string filename)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = filename };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(
                fileMap, ConfigurationUserLevel.None, true);
            var section = (UnityConfigurationSection)configuration.GetSection("unity");
            section.Configure(container);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }
    }
}
