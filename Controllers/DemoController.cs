namespace NorDev.Aop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class DemoController : Controller
    {
        private readonly IDemoService demoService;

        public DemoController(IDemoService demoService)
        {
            this.demoService = demoService;
        }

        [HttpGet]
        public string DoStuff(string id)
        {
            return this.demoService.DoStuff(id);
        }
    }
}