using Microsoft.AspNetCore.Mvc;

namespace SIENN.WebApi.Controllers
{
    //[Route("api/[controller]")]
    public class SiennController : Controller
    {
        [HttpGet]
        public string Get() => "SIENN Poland";
    }
}
