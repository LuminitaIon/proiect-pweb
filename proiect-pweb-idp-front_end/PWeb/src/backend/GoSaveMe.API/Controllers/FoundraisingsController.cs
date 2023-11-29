using GoSaveMe.Commons.Models.API;
using GoSaveMe.Commons.Models.Database;
using GoSaveMe.Commons.Processors;
using Microsoft.AspNetCore.Mvc;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoundraisingsController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public FoundraisingsController()
        {
            _logger = new(GetType().Name);
        }

        [HttpGet(Name = "GetFoundraisings")]
        public ActionResult<IResponse> Get()
        {
           _logger.Info("GetFoundraisings executed.");

            List<Foundraising>? frs = DatabaseProcessor.FoundraisingDB.Get();

            if (frs == null)
                return Ok(new SuccessResponse<object>(null));

            return Ok(new SuccessResponse<List<Foundraising>>(frs));
        }
    }
}
