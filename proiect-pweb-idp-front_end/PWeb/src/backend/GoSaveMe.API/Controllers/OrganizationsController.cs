using GoSaveMe.Commons.Models.API;
using GoSaveMe.Commons.Models.Database;
using GoSaveMe.Commons.Processors;
using Microsoft.AspNetCore.Mvc;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public OrganizationsController()
        {
            _logger = new(GetType().Name);
        }

        [HttpGet(Name = "GetOrganizations")]
        public ActionResult<IResponse> Get([FromQuery(Name = "username")] string? username)
        {
           _logger.Info("GetOrganizations executed.");

            List<User>? users = DatabaseProcessor.UserDB.Get(username, true);

            if (users == null)
                return Ok(new SuccessResponse<object>(null));

            if (users.Count == 1)
                return Ok(new SuccessResponse<User>(users.FirstOrDefault()));

            return Ok(new SuccessResponse<List<User>>(users));
        }
    }
}
