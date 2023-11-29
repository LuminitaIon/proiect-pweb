using GoSaveMe.Commons.Models;
using GoSaveMe.Commons.Models.API;
using GoSaveMe.Commons.Models.Database;
using GoSaveMe.Commons.Processors;
using Microsoft.AspNetCore.Mvc;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SafepointController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public SafepointController()
        {
            _logger = new(GetType().Name);
        }

        [HttpGet(Name = "GetSafepoints")]
        public ActionResult<IResponse> Get()
        {
            _logger.Info("GetSafepoints executed.");

            List<Safepoint>? safepoints = DatabaseProcessor.SafepointDB.Get();

            return Ok(new SuccessResponse<List<Safepoint>>(safepoints));
        }
    }
}
