using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DetailsController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public DetailsController()
        {
            _logger = new(GetType().Name);
        }

        [HttpGet(Name = "GetAppDetails")]
        public string Get()
        {
            _logger.Info("App details");

            return "App details";
        }
    }
}
