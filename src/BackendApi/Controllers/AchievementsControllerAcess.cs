using BackendApi.Contract.Achievements;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsControllerAcess : Controller
    {
        private IAchievementsService _achievementsService;
        public AchievementsControllerAcess(IAchievementsService achievementsService)
        {
            _achievementsService = achievementsService;
        }


        /// <summary>
        /// Получение всех ачивок 
        /// </summary>
        /// <param name="model">Ачивки</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _achievementsService.GetAll();
            GetAchievementsResponse[] createAchievementsRequests = new GetAchievementsResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createAchievementsRequests[i] = new GetAchievementsResponse()
                {
                    AchievementsId = item.AchievementsId,
                    AchievementsText = item.AchievementsText,
                    AchievementsType = item.AchievementsType,
                };
                i++;
            }
            return Ok(createAchievementsRequests);
        }


        /// <summary>
        /// Получение ачивки по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Achievements
        ///     {
        ///        AchievementsId = 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _achievementsService.GetById(id);
            var response = new GetAchievementsResponse()
            {
                AchievementsId = result.AchievementsId,
                AchievementsType = result.AchievementsType,
                AchievementsText = result.AchievementsText,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новой ачивки
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Achievements
        ///     {
        ///       "AchievementsId = 1,
        ///        AchievementsType = 1,
        ///        AchievementsText = "Черный мечник",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки</param>
        /// <returns></returns>

        // POST api/<AchievementsController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateAchievementsRequest request)
        {
            var achievement = request.Adapt<Achievement>();
            await _achievementsService.Create(achievement);
            return Ok();
        }

        /// <summary>
        /// Изменить данные ачивки
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Achievements
        ///     {
        ///        "AchievementsId = 1,
        ///         AchievementsType = 1,
        ///         AchievementsText = "Черный мечник",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки</param>
        /// <returns></returns>
        // POST api/<AchievementsController>

        [HttpPut]
        public async Task<IActionResult> Update(CreateAchievementsRequest result)
        {
            var achievement = result.Adapt<Achievement>();
            await _achievementsService.Update(achievement);
            return Ok(result);
        }

        /// <summary>
        /// Удалить ачивку по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Achievements
        ///     {
        ///        "AchievementsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Ачивки</param>
        /// <returns></returns>

        // POST api/<AchievementsController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetAchievementsResponse request = new GetAchievementsResponse();
            var achievement = request.Adapt<Achievement>();
            await _achievementsService.Delete(id);
            return Ok(achievement);
        }
    }
}