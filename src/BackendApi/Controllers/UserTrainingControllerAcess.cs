using BackendApi.Contract.Users;
using BackendApi.Contract.UserTraining;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTrainingControllerAcess : Controller
    {
        private IUserTrainingService _userTrainingService;
        public UserTrainingControllerAcess(IUserTrainingService userTrainingService)
        {
            _userTrainingService = userTrainingService;
        }

        /// <summary>
        /// Получение всех тренеровок пользователей
        /// </summary>
        /// <param name="model">Тренировки пользователя</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userTrainingService.GetAll();
            GetUserTrainingResponse[] createUserTrainingRequests = new GetUserTrainingResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createUserTrainingRequests[i] = new GetUserTrainingResponse()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    TrainerId = item.TrainerId,
                    TrainingId = item.TrainingId,
                    Duration = item.Duration,
                    Notes = item.Notes,
                    DayOfWeek = item.DayOfWeek,
                    TrainingStatus = item.TrainingStatus,
                    StartAt = item.StartAt,
                    EndAt = item.EndAt,
                };
                i++;
            }
            return Ok(createUserTrainingRequests);
        }


        /// <summary>
        /// Получение тренеровки пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserTraining
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренировки пользователя</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userTrainingService.GetById(id);
            var response = new GetUserTrainingResponse()
            {
                Id = result.Id,
                UserId = result.UserId,
                TrainerId = result.TrainerId,
                TrainingId = result.TrainingId,
                Duration = result.Duration,
                Notes = result.Notes,
                DayOfWeek = result.DayOfWeek,
                TrainingStatus = result.TrainingStatus,
                StartAt = result.StartAt,
                EndAt = result.EndAt,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новой тренеровки пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserTraining
        ///     {
        ///        "Id": 1,
        ///        "UserId": 1,
        ///        "TrainerId": 1,
        ///        "TrainingId": 1,
        ///        "Duration": "что угодно",
        ///        "Notes": "что угодно",
        ///        "DayOfWeek": "Понедельник",
        ///        "TrainingStatus": "'started','paused','finished'",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренировки пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserTrainingRequest request)
        {
            var userTraining = request.Adapt<UserTraining>();
            await _userTrainingService.Create(userTraining);
            return Ok();
        }

        /// <summary>
        /// Изменить данные тренеровки пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserTraining
        ///     {
        ///        "Id": 1,
        ///        "UserId": 1,
        ///        "TrainerId": 1,
        ///        "TrainingId": 1,
        ///        "Duration": "что угодно",
        ///        "Notes": "что угодно",
        ///        "DayOfWeek": "Понедельник",
        ///        "TrainingStatus": "'started','paused','finished'",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренировки пользователя</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetUserTrainingResponse result)
        {
            var userTraining = result.Adapt<UserTraining>();
            await _userTrainingService.Update(userTraining);
            return Ok(result);
        }

        /// <summary>
        /// Удалить тренеровоки пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserTraining
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренировки пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetUserTrainingResponse request = new GetUserTrainingResponse();
            var userTraining = request.Adapt<UserTraining>();
            await _userTrainingService.Delete(id);
            return Ok();
        }
    }
}