using GoSaveMe.Commons;
using Microsoft.AspNetCore.Mvc;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public HomeController()
        {
            _logger = new(GetType().Name);
        }

        [HttpGet(Name = "GetAppName")]
        public string Get()
        {
            _logger.Info("GetAppName executed.");

            return Constants.AppName;
        }
    }
}