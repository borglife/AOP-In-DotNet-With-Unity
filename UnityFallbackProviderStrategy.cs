namespace NorDev.Aop.Web
{
    using System;
    using Unity;
    using Unity.Builder;
    using Unity.Builder.Strategy;

    public class UnityFallbackProviderStrategy : BuilderStrategy
    {
        private readonly IUnityContainer container;

        public UnityFallbackProviderStrategy(IUnityContainer container)
        {
            this.container = container;
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            var key = context.OriginalBuildKey;

            // Checking if the Type we are resolving is registered with the Container
            if (!this.container.IsRegistered(key.Type))
            {
                // If not we first get our default IServiceProvider and then try to resolve the type with it
                // Then we save the Type in the Existing Property of IBuilderContext to tell Unity
                // that it doesn't need to resolve the Type
                context.Existing = this.container.Resolve<IServiceProvider>("UnityFallbackProvider").GetService(key.Type);
            }

            // Otherwise we do the default stuff
            base.PreBuildUp(context);
        }
    }
}