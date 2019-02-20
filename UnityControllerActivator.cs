namespace NorDev.Aop.Web
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Unity;

    public class UnityControllerActivator : IControllerActivator
    {
        private readonly IUnityContainer unityContainer;

        public UnityControllerActivator(IUnityContainer container)
        {
            this.unityContainer = container;
        }

        public object Create(ControllerContext context)
        {
            return this.unityContainer.Resolve(context.ActionDescriptor.ControllerTypeInfo.AsType());
        }

        public void Release(ControllerContext context, object controller)
        {
            // ignored
        }
    }
}