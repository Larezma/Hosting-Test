using BackendApi.Contract.Users;
using BackendApi.Contract.UserToDialogs;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToDialogsControllerAcess : Controller
    {
        private IUserToDialogsService _userToDialogsService;
        public UserToDialogsControllerAcess(IUserToDialogsService userToDialogsService)
        {
            _userToDialogsService = userToDialogsService;
        }

        /// <summary>
        /// Получение всех диалогов пользователей
        /// </summary>
        /// <param name="model">Диалоги пользователя</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userToDialogsService.GetAll();
            GetUserToDialogsResponse[] createUserRequests = new GetUserToDialogsResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createUserRequests[i] = new GetUserToDialogsResponse()
                {
                    Id = item.Id,
                    DialogId = item.DialogId,
                    UserId = item.UserId,
                    TimeCreate = item.TimeCreate,
                };
                i++;
            }
            return Ok(createUserRequests);
        }


        /// <summary>
        /// Получение диалогов пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToDialogs
        ///     {
        ///        "Id": 1,
        ///        "DialogId 1,
        ///        "UserId": 1,
        ///        "TimeCreate": "11:11:2022",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Диалоги пользователя</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userToDialogsService.GetById(id);
            var response = new GetUserToDialogsResponse()
            {
                Id = result.Id,
                DialogId = result.DialogId,
                UserId = result.UserId,
                TimeCreate = result.TimeCreate,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового диалога пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToDialogs
        ///     {
        ///        "Id": 1,
        ///        "DialogId": 1,
        ///        "UserId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Диалоги пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserToDialogsRequest request)
        {
            var userToDialog = request.Adapt<UserToDialog>();
            await _userToDialogsService.Create(userToDialog);
            return Ok();
        }

        /// <summary>
        /// Изменить данные диалога пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToDialogs
        ///     {
        ///        "Id": 1,
        ///        "DialogId": 1,
        ///        "UserId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Диалоги пользователя</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetUserToDialogsResponse result)
        {
            var userToDialog = result.Adapt<UserToDialog>();
            await _userToDialogsService.Update(userToDialog);
            return Ok(result);
        }

        /// <summary>
        /// Удалить диалога пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToDialogs
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Диалоги пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetUserToDialogsResponse request = new GetUserToDialogsResponse();
            var userToDialog = request.Adapt<UserToDialog>();
            await _userToDialogsService.Delete(id);
            return Ok();
        }
    }
}