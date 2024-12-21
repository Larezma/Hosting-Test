using BackendApi.Contract.Training;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingControllerAcess : Controller
    {
        private ITrainingService _trainingService;
        public TrainingControllerAcess(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        /// <summary>
        /// Получение всех тренировок
        /// </summary>
        /// <param name="model">Тренировки</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _trainingService.GetAll();
            GetTrainingResponse[] createTrainingRequests = new GetTrainingResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createTrainingRequests[i] = new GetTrainingResponse()
                {
                    TrainingId = item.TrainingId,
                    TrainingDate = item.TrainingDate,
                    DurationMinutes = item.DurationMinutes,
                    CaloriesBurned = item.CaloriesBurned,
                    Notes = item.Notes,
                    TrainingType = item.TrainingType,
                };
                i++;
            }
            return Ok(createTrainingRequests);
        }


        /// <summary>
        /// Получение тренировки по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Training
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренировки</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _trainingService.GetById(id);
            var response = new GetTrainingResponse()
            {
                TrainingId = result.TrainingId,
                TrainingDate = result.TrainingDate,
                DurationMinutes = result.DurationMinutes,
                CaloriesBurned = result.CaloriesBurned,
                Notes = result.Notes,
                TrainingType = result.TrainingType,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новой тренировки
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Training
        ///     {
        ///        "TrainingId": 1,
        ///        "TrainingDate": "11:11:2022",
        ///        "DurationMinutes": "120m",
        ///        "CaloriesBurned": 1233333,
        ///        "Notes": "sdsdsssdds",
        ///        "TrainingType": "saddsdds",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренировки</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateTrainingRequest request)
        {
            var training = request.Adapt<Training>();
            await _trainingService.Create(training);
            return Ok();
        }

        /// <summary>
        /// Изменить данные тренировки
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Training
        ///     {
        ///        "TrainingId": 1,
        ///        "TrainingDate": "11:11:2022",
        ///        "DurationMinutes": "120m",
        ///        "CaloriesBurned": 1233333,
        ///        "Notes": "sdsdsssdds",
        ///        "TrainingType": "saddsdds",
        ///     }
        ///
        ///
        /// </remarks>
        /// <param name="model">Тренировки</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetTrainingResponse result)
        {
            var training = result.Adapt<Training>();
            await _trainingService.Update(training);
            return Ok(result);
        }

        /// <summary>
        /// Удалить тренеровку по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Training
        ///     {
        ///        "TrainingId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренировки</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetTrainingResponse request = new GetTrainingResponse();
            var training = request.Adapt<Training>();
            await _trainingService.Delete(id);
            return Ok();
        }
    }
}