using BackendApi.Contract.Comments;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsControllerAcess : Controller
    {
        private ICommentsService _commentsService;
        public CommentsControllerAcess(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }


        /// <summary>
        /// Получение всех комментариев
        /// </summary>
        /// <param name="model">Комментарии</param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _commentsService.GetAll();
            GetCommentsResponse[] createCommentsRequests = new GetCommentsResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createCommentsRequests[i] = new GetCommentsResponse()
                {
                    CommentsId = item.CommentsId,
                    UserId = item.UserId,
                    ItemId = item.ItemId,
                    ItemType = item.ItemType,
                    CommentsText = item.CommentsText,
                    CommentsDate = item.CommentsDate,
                };
                i++;
            }
            return Ok(createCommentsRequests);
        }


        /// <summary>
        /// Получение комментария по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Comments
        ///     {
        ///        "CommentsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Комментарии</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _commentsService.GetById(id);
            var response = new GetCommentsResponse()
            {
                CommentsId = result.CommentsId,
                UserId = result.UserId,
                ItemId = result.ItemId,
                ItemType = result.ItemType,
                CommentsText = result.CommentsText,
                CommentsDate = result.CommentsDate,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Comments
        ///     {
        ///        "CommentsId": 1,
        ///        "UserId": 1,
        ///        "ItemId": 1,
        ///        "ItemType": "ssdds",
        ///        "CommentsText": "фывыввы",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Комментарии</param>
        /// <returns></returns>

        // POST api/<CommentsController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateCommentsRequest request)
        {
            var comment = request.Adapt<Comment>();
            await _commentsService.Create(comment);
            return Ok();
        }

        /// <summary>
        /// Изменить данные комментария
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Comments
        ///     {
        ///        "CommentsId": 1,
        ///        "UserId": 1,
        ///        "ItemId": 1,
        ///        "ItemType": "ssdds",
        ///        "CommentsText": "фывыввы",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Комментарии</param>
        /// <returns></returns>
        // POST api/<CommentsController>

        [HttpPut]
        public async Task<IActionResult> Update(GetCommentsResponse result)
        {
            var comment = result.Adapt<Comment>();
            await _commentsService.Update(comment);
            return Ok(result);
        }

        /// <summary>
        /// Удалить комментарий по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Users
        ///     {
        ///        "CommentsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Комментарии</param>
        /// <returns></returns>

        // POST api/<CommentsController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetCommentsResponse request = new GetCommentsResponse();
            var comment = request.Adapt<Comment>();
            await _commentsService.Delete(id);
            return Ok();
        }
    }
}