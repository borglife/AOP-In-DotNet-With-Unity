namespace NorDev.Aop.Web
{
    using Unity.Interception.PolicyInjection.Pipeline;

    public class LoggingCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(
                $"{input.MethodBase.Name} is being called at {System.DateTime.UtcNow}");
            var result = getNext().Invoke(input, getNext);
            logger.Info(
                $"{input.MethodBase.Name} finished being called at {System.DateTime.UtcNow}");

            if (result.Exception != null)
            {
                logger.Error($"'{result.Exception}' was thrown '{result.Exception.Message}'");
            }

            return result;
        }

        public int Order { get; set; }
    }
}