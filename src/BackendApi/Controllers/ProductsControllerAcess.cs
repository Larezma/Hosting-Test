using BackendApi.Contract.Products;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsControllerAcess : Controller
    {
        private IProductsService _productsService;
        public ProductsControllerAcess(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Получение всех продукт
        /// </summary>
        /// <param name="model">Продукты</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productsService.GetAll();
            GetProductsResponse[] createProcursRequests = new GetProductsResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createProcursRequests[i] = new GetProductsResponse()
                {
                    ProductId = item.ProductId,
                    Product1 = item.Product1,
                    Calories = item.Calories,
                    ProteinPer = item.ProteinPer,
                    FatPer = item.FatPer,
                    CarbsPer = item.CarbsPer,
                    VitaminsAndMinerals = item.VitaminsAndMinerals,
                    NutritionalValue = item.NutritionalValue,
                };
                i++;
            }
            return Ok(createProcursRequests);
        }


        /// <summary>
        /// Получение продукта по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Products
        ///     {
        ///        "ProductId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Продукты</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productsService.GetById(id);
            var response = new GetProductsResponse()
            {
                ProductId = result.ProductId,
                Product1 = result.Product1,
                Calories = result.Calories,
                ProteinPer = result.ProteinPer,
                FatPer = result.FatPer,
                CarbsPer = result.CarbsPer,
                VitaminsAndMinerals = result.VitaminsAndMinerals,
                NutritionalValue = result.NutritionalValue,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового продукта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Products
        ///     {
        ///        "ProductId": 1,
        ///        "Product1": 1,
        ///        "Calories": 1,1111232,
        ///        "ProteinPer": 1,1111232,
        ///        "FatPer": 1,1111232,
        ///        "CarbsPer": 1,1111232,
        ///        "VitaminsAndMinerals": 1,1111232,
        ///        "NutritionalValue": "sdsdsdssd",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Продукты</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductsRequest request)
        {
            var product = request.Adapt<Product>();
            await _productsService.Create(product);
            return Ok();
        }

        /// <summary>
        /// Изменить данные продукта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Products
        ///     {
        ///        "ProductId": 1,
        ///        "Product1": 1,
        ///        "Calories": 1,1111232,
        ///        "ProteinPer": 1,1111232,
        ///        "FatPer": 1,1111232,
        ///        "CarbsPer": 1,1111232,
        ///        "VitaminsAndMinerals": 1,1111232,
        ///        "NutritionalValue": "sdsdsdssd",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Продукты</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetProductsResponse result)
        {
            var product = result.Adapt<Product>();
            await _productsService.Update(product);
            return Ok(result);
        }

        /// <summary>
        /// Удалить продукта по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Products
        ///     {
        ///        "ProductId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Продукты</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetProductsResponse request = new GetProductsResponse();
            var product = request.Adapt<Product>();
            await _productsService.Delete(id);
            return Ok();
        }
    }
}