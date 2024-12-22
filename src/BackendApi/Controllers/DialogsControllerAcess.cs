using BackendApi.Contract.Comments;
using BackendApi.Contract.Dialogs;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsControllerAcess : Controller
    {
        private IDialogsService _dialogsService;
        public DialogsControllerAcess(IDialogsService dialogsService)
        {
            _dialogsService = dialogsService;
        }

        /// <summary>
        /// Получение всех диалогов
        /// </summary>
        /// <param name="model">Диалоги</param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dialogsService.GetAll();
            GetDialogsResponse[] createDialogsRequests = new GetDialogsResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createDialogsRequests[i] = new GetDialogsResponse()
                {
                    DialogsId = item.DialogsId,
                    TextDialogs = item.TextDialogs,
                    TimeCreate = item.TimeCreate,
                    EndTime = item.EndTime,
                };
                i++;
            }
            return Ok(createDialogsRequests);
        }


        /// <summary>
        /// Получение диалога по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Dialogs
        ///     {
        ///        "DialogsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Диалоги</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dialogsService.GetById(id);
            var response = new GetDialogsResponse()
            {
                DialogsId = result.DialogsId,
                TextDialogs = result.TextDialogs,
                TimeCreate = result.TimeCreate,
                EndTime = result.EndTime,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новый диалога
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Dialogs
        ///     {
        ///        "DialogsId": 1,
        ///        "TextDialogs": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Диалоги</param>
        /// <returns></returns>

        // POST api/<CommentsController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateDialogsRequest request)
        {
            var dialog = request.Adapt<Dialog>();
            await _dialogsService.Create(dialog);
            return Ok();
        }

        /// <summary>
        /// Изменить данные диалога
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Dialogs
        ///     {
        ///        "DialogsId": 1,
        ///        "TextDialogs": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Диалоги</param>
        /// <returns></returns>
        // POST api/<CommentsController>

        [HttpPut]
        public async Task<IActionResult> Update(GetDialogsResponse result)
        {
            var dialog = result.Adapt<Dialog>();
            await _dialogsService.Update(dialog);
            return Ok(result);
        }

        /// <summary>
        /// Удалить диалог по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Dialogs
        ///     {
        ///        "DialogsId": 1,
        ///     }
        ///
        ///
        /// </remarks>
        /// <param name="model">Диалоги</param>
        /// <returns></returns>

        // POST api/<CommentsController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetDialogsResponse request = new GetDialogsResponse();
            var dialog = request.Adapt<Dialog>();
            await _dialogsService.Delete(id);
            return Ok();
        }
    }
}