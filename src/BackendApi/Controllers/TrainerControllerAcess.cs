using BackendApi.Contract.Trainer;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerControllerAcess : Controller
    {
        private ITrainerService _trainerService;
        public TrainerControllerAcess(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        /// <summary>
        /// Получение всех тренеров
        /// </summary>
        /// <param name="model">Тренеры</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _trainerService.GetAll();
            GetTrainerResponse[] createTrainerRequests = new GetTrainerResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createTrainerRequests[i] = new GetTrainerResponse()
                {
                    TrainerId = item.TrainerId,
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    Password = item.Password,
                    CreateAt = item.CreateAt,
                };
                i++;
            }
            return Ok(createTrainerRequests);
        }


        /// <summary>
        /// Получение тренера по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Trainer
        ///     {
        ///        "TrainerId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренеры</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _trainerService.GetById(id);
            var response = new GetTrainerResponse()
            {
                TrainerId = result.TrainerId,
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                Password = result.Password,
                CreateAt = result.CreateAt,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового тренера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Trainer
        ///     {
        ///        "TrainerId": 1,
        ///        "UserId": 1,
        ///        "FirstName": "Иванов",
        ///        "MiddleName": "Иван",
        ///        "LastName": "Иванович",
        ///        "Email": "samdsdos@sds.com",
        ///        "phoneNumber": "+70000000000",
        ///        "Password": "123423323432134",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренеры</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateTrainerRequest request)
        {
            var trainer = request.Adapt<Trainer>();
            await _trainerService.Create(trainer);
            return Ok();
        }

        /// <summary>
        /// Изменить данные тренера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Trainer
        ///     {
        ///        "TrainerId": 1,
        ///        "UserId": 1,
        ///        "FirstName": "Иванов",
        ///        "MiddleName": "Иван",
        ///        "LastName": "Иванович",
        ///        "Email": "samdsdos@sds.com",
        ///        "phoneNumber": "+70000000000",
        ///        "Password": "123423323432134",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренеры</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetTrainerResponse result)
        {
            var trainer = result.Adapt<Trainer>();
            await _trainerService.Update(trainer);
            return Ok(result);
        }

        /// <summary>
        /// Удалить тренара по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Trainer
        ///     {
        ///        "TrainerId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Тренеры</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetTrainerResponse request = new GetTrainerResponse();
            var trainer = request.Adapt<Trainer>();
            await _trainerService.Delete(id);
            return Ok();
        }
    }
}