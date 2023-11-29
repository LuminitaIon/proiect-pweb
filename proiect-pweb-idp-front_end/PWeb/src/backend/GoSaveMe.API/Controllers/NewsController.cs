using GoSaveMe.Commons;
using GoSaveMe.Commons.ExtensionMethods;
using GoSaveMe.Commons.Models.API;
using GoSaveMe.Commons.Models.API.Payload;
using GoSaveMe.Commons.Models.Database;
using GoSaveMe.Commons.Processors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GoSaveMe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly Commons.Logger.Logger _logger;
        public NewsController()
        {
            _logger = new(GetType().Name);
        }

        [HttpGet(Name = "GetNews")]
        public ActionResult<IResponse> Get([FromQuery(Name = "approved")] bool approved, [FromQuery(Name = "username")] string? username)
        {
            _logger.Info("GetNews executed.");

            if (approved && string.IsNullOrEmpty(username))
            {
                try
                {
                    List<News>? news = DatabaseProcessor.NewsDB.GetFilteredNews(approved, username);

                    return Ok(new SuccessResponse<List<News>>(news));
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);

                    return Ok(new ErrorResponse(ex.Message));
                }
            }
            else
            {
                if (approved)
                {
                    try
                    {
                        List<News>? news = DatabaseProcessor.NewsDB.GetFilteredNews(approved, username);

                        return Ok(new SuccessResponse<List<News>>(news));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);

                        return Ok(new ErrorResponse(ex.Message));
                    }
                }
                else
                {
                    try
                    {
                        List<News>? news = DatabaseProcessor.NewsDB.GetFilteredNews(approved, username);

                        return Ok(new SuccessResponse<List<News>>(news));
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message);

                        return Ok(new ErrorResponse(ex.Message));
                    }
                }
            }
        }

        [HttpPost(Name = "CreateNews")]
        [Authorize("AuthenticatedAccess")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IResponse> Post(NewsCreatePayload payload)
        {
            _logger.Info($"Create News executed | Payload: {JsonSerializer.Serialize(payload)}");

            try
            {
                string missingFields = string.Empty;

                if (payload == null)
                    return BadRequest(new ErrorResponse("Missing payload."));
                if (string.IsNullOrWhiteSpace(payload.Title))
                    return BadRequest(new ErrorResponse($"Missing {nameof(payload.Title).LowerFirstLetter()}"));
                if (string.IsNullOrWhiteSpace(payload.Text))
                    return BadRequest(new ErrorResponse($"Missing {nameof(payload.Text).LowerFirstLetter()}"));
                if (payload.Username == null)
                    return BadRequest(new ErrorResponse($"Missing {nameof(payload.Username).LowerFirstLetter()}"));

                News? news = DatabaseProcessor.NewsDB.Create(payload.Title, payload.Text, payload.ImageURI, payload.Username, payload.Approved);

                if (payload.Approved)
                {
                    MessageQueueProcessor.Publish("NEWS_POSTED", payload.Username);
                }
                else
                {
                    MessageQueueProcessor.Publish("NEWS_QUEUED", payload.Username);
                }

                return Ok(new SuccessResponse<News>(news));
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

        [HttpPut(Name = "ApproveNews")]
        [Authorize("NewsModificationAccess")]
        public ActionResult<IResponse> Put([FromQuery(Name = "id")] int id)
        {
            _logger.Info($"Approve news | Id: {id}");

            try
            {
                News? news = DatabaseProcessor.NewsDB.Approve(id);

                return Ok(new SuccessResponse<News>(news));
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

        [HttpDelete(Name = "DeleteNews")]
        [Authorize("NewsModificationAccess")]
        public ActionResult<IResponse> Delete([FromQuery(Name = "id")] int id)
        {
            _logger.Info($"Delete news | Id: {id}");

            try
            {
                DatabaseProcessor.NewsDB.Delete(id);

                return Ok(new SuccessResponse<object>(true));
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
