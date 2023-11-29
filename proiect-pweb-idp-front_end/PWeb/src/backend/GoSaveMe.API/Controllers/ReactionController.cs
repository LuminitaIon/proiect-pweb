using GoSaveMe.Commons;
using GoSaveMe.Commons.ExtensionMethods;
using GoSaveMe.Commons.Models.API;
using GoSaveMe.Commons.Models.API.Payload;
using GoSaveMe.Commons.Processors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public ReactionController()
        {
            _logger = new(GetType().Name);
        }

        [HttpPost(Name = "PostReaction")]
        [Authorize("AuthenticatedAccess")]
        public ActionResult<IResponse> Post(CreateReactionPayload payload)
        {
            _logger.Info("PostReaction executed.");

            try
            {
                if (payload == null)
                    return BadRequest(new ErrorResponse("Missing payload."));
                if (string.IsNullOrWhiteSpace(payload.Username))
                    return BadRequest(new ErrorResponse($"Missing {nameof(payload.Username).LowerFirstLetter()}"));
                if (string.IsNullOrWhiteSpace(payload.Reaction))
                    return BadRequest(new ErrorResponse($"Missing {nameof(payload.Reaction).LowerFirstLetter()}"));
                if (payload.NewsId == null)
                    return BadRequest(new ErrorResponse($"Missing {nameof(payload.NewsId).LowerFirstLetter()}"));

                DatabaseProcessor.NewsReactionDB.Create(payload.NewsId.Value, payload.Username, payload.Reaction);

                return Ok(new SuccessResponse<bool>(true));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return new ErrorResponse()
                {
                    Message = ex.Message
                };
            }
        }

        [HttpDelete(Name = "DeleteReaction")]
        [Authorize("AuthenticatedAccess")]
        public ActionResult<IResponse> Delete([FromQuery(Name="newsId")] int newsId, [FromQuery(Name = "username")] string? username, [FromQuery(Name = "reaction")] string? reaction)
        {
            _logger.Info("PostReaction executed.");

            try
            {
                if (string.IsNullOrWhiteSpace(reaction))
                    return BadRequest(new ErrorResponse($"Missing {nameof(reaction).LowerFirstLetter()}"));
                if (string.IsNullOrWhiteSpace(username))
                    return BadRequest(new ErrorResponse($"Missing {nameof(username).LowerFirstLetter()}"));

                DatabaseProcessor.NewsReactionDB.Delete(newsId, username, reaction);

                return Ok(new SuccessResponse<bool>(true));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return new ErrorResponse()
                {
                    Message = ex.Message
                };
            }
        }
    }
}
