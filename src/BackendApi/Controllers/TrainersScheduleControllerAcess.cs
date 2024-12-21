using BackendApi.Contract.TrainersSchedule;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersScheduleControllerAcess : Controller
    {
        private ITrainersScheduleService _trainersScheduleService;
        public TrainersScheduleControllerAcess(ITrainersScheduleService trainersScheduleService)
        {
            _trainersScheduleService = trainersScheduleService;
        }

        /// <summary>
        /// Получение всех расписание тренеров
        /// </summary>
        /// <param name="model">Расписание тренеров</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _trainersScheduleService.GetAll();
            GetTrainersScheduleResponse[] createTrainersScheduleResponseRequests = new GetTrainersScheduleResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createTrainersScheduleResponseRequests[i] = new GetTrainersScheduleResponse()
                {
                    Id = item.Id,
                    ScheduleId = item.ScheduleId,
                    TrainerId = item.TrainerId,
                    TypeOfTraining = item.TypeOfTraining,
                    Date = item.Date,
                    Time = item.Time,
                    CreateAt = item.CreateAt,
                };
                i++;
            }
            return Ok(createTrainersScheduleResponseRequests);
        }


        /// <summary>
        /// Получение расписание тренеров по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /TrainersSchedule
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписание тренеров</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _trainersScheduleService.GetById(id);
            var response = new GetTrainersScheduleResponse()
            {
                Id = result.Id,
                ScheduleId = result.ScheduleId,
                TrainerId = result.TrainerId,
                TypeOfTraining = result.TypeOfTraining,
                Date = result.Date,
                Time = result.Time,
                CreateAt = result.CreateAt,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового расписание тренеров
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /TrainersSchedule
        ///     {
        ///        "Id": 1,
        ///        "ScheduleId": 1,
        ///        "TrainerId": 1,
        ///        "TypeOfTraining": "Кардио",
        ///        "Date": "11:11:2022",
        ///        "Time": "11:11:2022:11:11:11:11"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписание тренеров</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateTrainersScheduleRequest request)
        {
            var trainersSchedule = request.Adapt<TrainersSchedule>();
            await _trainersScheduleService.Create(trainersSchedule);
            return Ok();
        }

        /// <summary>
        /// Изменить данные расписание тренеров
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /TrainersSchedule
        ///     {
        ///        "Id": 1,
        ///        "ScheduleId": 1,
        ///        "TrainerId": 1,
        ///        "TypeOfTraining": "Кардио",
        ///        "Date": "11:11:2022",
        ///        "Time": "11:11:2022:11:11:11:11"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписание тренеров</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetTrainersScheduleResponse result)
        {
            var trainersSchedule = result.Adapt<TrainersSchedule>();
            await _trainersScheduleService.Update(trainersSchedule);
            return Ok(result);
        }

        /// <summary>
        /// Удалить расписание тренеров по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /TrainersSchedule
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписание тренеров</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetTrainersScheduleResponse request = new GetTrainersScheduleResponse();
            var trainersSchedule = request.Adapt<TrainersSchedule>();
            await _trainersScheduleService.Delete(id);
            return Ok();
        }
    }
}