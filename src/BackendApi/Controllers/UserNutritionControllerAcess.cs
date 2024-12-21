using BackendApi.Contract.UserNutrition;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNutritionControllerAcess : Controller
    {
        private IUserNutritionService _userNutritionService;
        public UserNutritionControllerAcess(IUserNutritionService userNutritionService)
        {
            _userNutritionService = userNutritionService;
        }

        /// <summary>
        /// Получение всех питания пользователей
        /// </summary>
        /// <param name="model">Питание пользователя</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userNutritionService.GetAll();
            GetUserNutritionResponse[] createUserNutritionRequests = new GetUserNutritionResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createUserNutritionRequests[i] = new GetUserNutritionResponse()
                {
                    NutritionId = item.NutritionId,
                    UserId = item.UserId,
                    UserNutritionId = item.NutritionId,
                    DateOfAdmission = item.DateOfAdmission,
                    AppointmentTime = item.AppointmentTime,
                    NutritionType = item.NutritionType,
                    Report = item.Report,
                    CreateAt = item.CreateAt,
                };
                i++;
            }
            return Ok(createUserNutritionRequests);
        }


        /// <summary>
        /// Получение питания пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Users
        ///     {
        ///        "userId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питание пользователя</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userNutritionService.GetById(id);
            var response = new GetUserNutritionResponse()
            {
                NutritionId = result.NutritionId,
                UserId = result.UserId,
                UserNutritionId = result.NutritionId,
                DateOfAdmission = result.DateOfAdmission,
                AppointmentTime = result.AppointmentTime,
                NutritionType = result.NutritionType,
                Report = result.Report,
                CreateAt = result.CreateAt,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новое питания пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Users
        ///     {
        ///        "userId": 1,
        ///        "userName": "GG",
        ///        "email": "ssd@gmail.com",
        ///        "password": "12345",
        ///        "roleUser": 0,(Не объязательно!),
        ///        "userImg": null,(Не объязательно!),
        ///        "phoneNumber": "123444444",
        ///        "aboutMe": null,(Не объязательно!),
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питание пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserNutritionRequest request)
        {
            var userNutrition = request.Adapt<UserNutrition>();
            await _userNutritionService.Create(userNutrition);
            return Ok();
        }

        /// <summary>
        /// Изменить данные питания пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Users
        ///     {
        ///        "userId": 1,
        ///        "userName": "GG",
        ///        "email": "ssd@gmail.com",
        ///        "password": "12345",
        ///        "roleUser": 0,(Не объязательно!),
        ///        "userImg": null,(Не объязательно!),
        ///        "phoneNumber": "123444444",
        ///        "aboutMe": null,(Не объязательно!),
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питание пользователя</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetUserNutritionResponse result)
        {
            var userNutrition = result.Adapt<UserNutrition>();
            await _userNutritionService.Update(userNutrition);
            return Ok(result);
        }

        /// <summary>
        /// Удалить питания пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Users
        ///     {
        ///        "userId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питание пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetUserNutritionResponse request = new GetUserNutritionResponse();
            var userNutrition = request.Adapt<UserNutrition>();
            await _userNutritionService.Delete(id);
            return Ok();
        }
    }
}