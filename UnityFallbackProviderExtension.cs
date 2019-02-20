namespace NorDev.Aop.Web
{
    using System;
    using Unity;
    using Unity.Builder;
    using Unity.Extension;

    // From https://stackoverflow.com/a/39173346 
    public class UnityFallbackProviderExtension : UnityContainerExtension
    {
        private readonly IServiceProvider defaultServiceProvider;

        public UnityFallbackProviderExtension(IServiceProvider defaultServiceProvider)
        {
            this.defaultServiceProvider = defaultServiceProvider;
        }

        protected override void Initialize()
        {
            this.Context.Container.RegisterInstance("UnityFallbackProvider", this.defaultServiceProvider);

            var strategy = new UnityFallbackProviderStrategy(this.Context.Container);

            // Adding the UnityFallbackProviderStrategy to be executed with the PreCreation LifeCycleHook
            // PreCreation because if it isnt registerd with the IUnityContainer there will be an Exception
            // Now if the IUnityContainer "magically" gets a Instance of a Type it will accept it and move on
            this.Context.Strategies.Add(strategy, UnityBuildStage.PreCreation);
        }
    }
}