using BackendApi.Contract.Schedule;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleControllerAcess : Controller
    {
        private IScheduleService _scheduleService;
        public ScheduleControllerAcess(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// Получение всех расписаний 
        /// </summary>
        /// <param name="model">Расписания</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _scheduleService.GetAll();
            GetScheduleResponse[] createScheduleRequests = new GetScheduleResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createScheduleRequests[i] = new GetScheduleResponse()
                {
                    ScheduleId = item.ScheduleId,
                    TrainingId = item.TrainingId,
                    TrainerId = item.TrainerId,
                    TrainingType = item.TrainingType,
                    DayOfWeek = item.DayOfWeek,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                };
                i++;
            }
            return Ok(createScheduleRequests);
        }


        /// <summary>
        /// Получение расписания по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Schedule
        ///     {
        ///        "ScheduleId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписания</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _scheduleService.GetById(id);
            var response = new GetScheduleResponse()
            {
                ScheduleId = result.ScheduleId,
                TrainingId = result.TrainingId,
                TrainerId = result.TrainerId,
                TrainingType = result.TrainingType,
                DayOfWeek = result.DayOfWeek,
                StartTime = result.StartTime,
                EndTime = result.EndTime,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового расписания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Schedule
        ///     {
        ///        "ScheduleId": 1,
        ///        "TrainingId": 1,
        ///        "TrainerId": 1,
        ///        "TrainingType": "Силовая",
        ///        "DayOfWeek": "понедельник",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписания</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateScheduleRequest request)
        {
            var schedule = request.Adapt<Schedule>();
            await _scheduleService.Create(schedule);
            return Ok();
        }

        /// <summary>
        /// Изменить данные расписания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Schedule
        ///     {
        ///        "ScheduleId": 1,
        ///        "TrainingId": 1,
        ///        "TrainerId": 1,
        ///        "TrainingType": "Силовая",
        ///        "DayOfWeek": "понедельник",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписания</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetScheduleResponse result)
        {
            var schedule = result.Adapt<Schedule>();
            await _scheduleService.Update(schedule);
            return Ok(result);
        }

        /// <summary>
        /// Удалить расписание по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Schedule
        ///     {
        ///        "ScheduleId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Расписания</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetScheduleResponse request = new GetScheduleResponse();
            var schedule = request.Adapt<Schedule>();
            await _scheduleService.Delete(id);
            return Ok();
        }
    }
}