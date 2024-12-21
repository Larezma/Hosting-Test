using BackendApi.Contract.Users;
using BackendApi.Contract.UserToAchievements;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToAchievementsControllerAcess : Controller
    {
        private IUserToAchievementService _userToAchievementService;
        public UserToAchievementsControllerAcess(IUserToAchievementService userToAchievementService)
        {
            _userToAchievementService = userToAchievementService;
        }
        /// <summary>
        /// Получение всех ачивки пользователей
        /// </summary>
        /// <param name="model">Ачивки пользователя</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userToAchievementService.GetAll();
            GetUserToAchievementsResponse[] createUserToAchievementsRequests = new GetUserToAchievementsResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createUserToAchievementsRequests[i] = new GetUserToAchievementsResponse()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    AchievementsId = item.AchievementsId,
                    GetDateAchievements = item.GetDateAchievements,
                };
                i++;
            }
            return Ok(createUserToAchievementsRequests);
        }


        /// <summary>
        /// Получение ачивки пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToAchievements
        ///     {
        ///        "Id": 1,
        ///        "UserId": 1,
        ///        "AchievementsId": 1,
        ///        "GetDateAchievements": "11:11:2020",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки пользователя</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userToAchievementService.GetById(id);
            var response = new GetUserToAchievementsResponse()
            {
                Id = result.Id,
                UserId = result.UserId,
                AchievementsId = result.AchievementsId,
                GetDateAchievements = result.GetDateAchievements,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новую ачивку пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToAchievements
        ///     {
        ///        "Id": 1,
        ///        "UserId": 1,
        ///        "AchievementsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserToAchievementsRequest request)
        {
            var userToAchievement = request.Adapt<UserToAchievement>();
            await _userToAchievementService.Create(userToAchievement);
            return Ok();
        }

        /// <summary>
        /// Изменить данные ачивки пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToAchievements
        ///     {
        ///        "Id": 1,
        ///        "UserId": 1,
        ///        "AchievementsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки пользователя</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetUserToAchievementsResponse result)
        {
            var userToAchievement = result.Adapt<UserToAchievement>();
            await _userToAchievementService.Update(userToAchievement);
            return Ok(result);
        }

        /// <summary>
        /// Удалить ачивку пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToAchievements
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetUserToAchievementsResponse request = new GetUserToAchievementsResponse();
            var userToAchievement = request.Adapt<UserToAchievement>();
            await _userToAchievementService.Delete(id);
            return Ok();
        }
    }
}