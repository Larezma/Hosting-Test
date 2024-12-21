using BackendApi.Contract.Friend;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendControllerAcess : Controller
    {
        private IFriendService _friendService;
        public FriendControllerAcess(IFriendService friendService)
        {
            _friendService = friendService;
        }

        /// <summary>
        /// Получение списка всех друзей в таблице
        /// </summary>
        /// <param name="model">Друзья</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _friendService.GetAll();
            GetFriendResponse[] createFriendRequests = new GetFriendResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createFriendRequests[i] = new GetFriendResponse()
                {
                    FriendId = item.FriendId,
                    UserId1 = item.UserId1,
                    UserId2 = item.UserId2,
                    StartDate = DateTime.Now,
                };
                i++;
            }
            return Ok(createFriendRequests);
        }

        /// <summary>
        /// Получение друга по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Friend
        ///     {
        ///        "FriendId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Друзья</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _friendService.GetById(id);
            var response = new GetFriendResponse()
            {
                FriendId = result.FriendId,
                UserId1 = result.UserId1,
                UserId2 = result.UserId2,
                StartDate = result.StartDate,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового друга
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Friend
        ///     {
        ///         "FriendId": 1,
        ///         "UserId1": 1,
        ///         "UserId2": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Друзья</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateFriendRequest request)
        {
            var friend = request.Adapt<Friend>();
            await _friendService.Create(friend);
            return Ok();
        }

        /// <summary>
        /// Изменить данные друга
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Friend
        ///     {
        ///         "FriendId": 1,
        ///         "UserId1": 1,
        ///         "UserId2": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Друзья</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetFriendResponse result)
        {
            var friend = result.Adapt<Friend>();
            await _friendService.Update(friend);
            return Ok(result);
        }

        /// <summary>
        /// Удалить друга по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Friend
        ///     {
        ///         "FriendId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Друзья</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetFriendResponse request = new GetFriendResponse();
            var friend = request.Adapt<Friend>();
            await _friendService.Delete(id);
            return Ok();
        }
    }
}