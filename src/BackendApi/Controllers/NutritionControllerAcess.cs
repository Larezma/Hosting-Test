using BackendApi.Contract.Nutrition;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionControllerAcess : Controller
    {
        private INutritionService _nutritionService;
        public NutritionControllerAcess(INutritionService nutritionService)
        {
            _nutritionService = nutritionService;
        }


        /// <summary>
        /// Получение всех питаний пользователя
        /// </summary>
        /// <param name="model">Питание</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _nutritionService.GetAll();
            GetNutritionResponse[] createNutritionRequests = new GetNutritionResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createNutritionRequests[i] = new GetNutritionResponse()
                {
                    NutritionId = item.NutritionId,
                    Product = item.Product,
                    MeanType = item.MeanType,
                    MeanDeacription = item.MeanDeacription,
                    DateNutrition = item.DateNutrition,
                };
                i++;
            }
            return Ok(createNutritionRequests);
        }


        /// <summary>
        /// Получение питание по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Nutrition
        ///     {
        ///        "NutritionId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питания</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _nutritionService.GetById(id);
            var response = new GetNutritionResponse()
            {
                NutritionId = result.NutritionId,
                Product = result.Product,
                MeanType = result.MeanType,
                MeanDeacription = result.MeanDeacription,
                DateNutrition = result.DateNutrition,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового питания
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Nutrition
        ///     {
        ///        "NutritionId": 1,
        ///        "Product": "1",
        ///        "MeanType": "вывывы",
        ///        "MeanDeacription": "ывывыв",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питания</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateNutritionRequest request)
        {
            var nutrition = request.Adapt<Nutrition>();
            await _nutritionService.Create(nutrition);
            return Ok();
        }

        /// <summary>
        /// Изменить данные питание
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Nutrition
        ///     {
        ///        "NutritionId": 1,
        ///        "Product": "1",
        ///        "MeanType": "вывывы",
        ///        "MeanDeacription": "ывывыв",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питания</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetNutritionResponse result)
        {
            var nutrition = result.Adapt<Nutrition>();
            await _nutritionService.Update(nutrition);
            return Ok(result);
        }

        /// <summary>
        /// Удалить питание по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Nutrition
        ///     {
        ///        "NutritionId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Питания</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetNutritionResponse request = new GetNutritionResponse();
            var nutrition = request.Adapt<Nutrition>();
            await _nutritionService.Delete(id);
            return Ok();
        }
    }
}