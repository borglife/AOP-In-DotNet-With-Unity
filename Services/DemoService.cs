namespace NorDev.Aop.Web.Services
{
    public class DemoService : IDemoService
    {
        public string DoStuff(string id)
        {
            return $"I did stuff to '{id}'";
        }
    }
}