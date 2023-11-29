using GoSaveMe.Commons.Models;
using GoSaveMe.Commons.Models.API;
using GoSaveMe.Commons.Models.API.Payload;
using GoSaveMe.Commons.Models.Database;
using GoSaveMe.Commons.Processors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public UserController()
        {
            _logger = new(GetType().Name);
        }

        [HttpGet(Name = "GetUser")]
        public ActionResult<IResponse> Get([FromQuery(Name = "username")] string? username, [FromQuery(Name = "isorg")] bool? isOrg)
        {
            _logger.Info("GetUser executed.");

            List<User>? users = DatabaseProcessor.UserDB.Get(username, null);

            if (users == null || users.Count > 1)
                return Ok(new SuccessResponse<object>(null));

            return Ok(new SuccessResponse<User>(users.FirstOrDefault()));
        }

        [HttpPost(Name = "CreateUser")]
        [Authorize("AuthenticatedAccess")]
        public ActionResult<IResponse> Post([FromQuery(Name = "username")] string username)
        {
            _logger.Info("CreateUser executed.");

            if (string.IsNullOrWhiteSpace(username))
            {
                return Ok(new SuccessResponse<object>(null));
            }

            User? user = DatabaseProcessor.UserDB.Create(username);

            MessageQueueProcessor.Publish("PROFILE_CREATED", username);

            return Ok(new SuccessResponse<User>(user));
        }

        [HttpPut(Name = "UpdateUser")]
        [Authorize("AuthenticatedAccess")]
        public ActionResult<IResponse> Put(User user)
        {
            _logger.Info("UpdateUser executed.");

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                return Ok(new SuccessResponse<object>(null));
            }

            User? updatedUser = DatabaseProcessor.UserDB.Update(user);

            MessageQueueProcessor.Publish("PROFILE_UPDATED", user.Username);

            return Ok(new SuccessResponse<User>(updatedUser));
        }
    }
}
