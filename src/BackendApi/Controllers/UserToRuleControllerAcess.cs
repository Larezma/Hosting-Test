using BackendApi.Contract.Users;
using BackendApi.Contract.UserToRule;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToRuleControllerAcess : Controller
    {
        private IUserToRuleService _userToRuleService;
        public UserToRuleControllerAcess(IUserToRuleService userToRuleService)
        {
            _userToRuleService = userToRuleService;
        }

        /// <summary>
        /// Получение всех ролей пользователей
        /// </summary>
        /// <param name="model">Роли пользователя</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userToRuleService.GetAll();
            GetUserToRuleResponse[] createUserToRuleRequests = new GetUserToRuleResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createUserToRuleRequests[i] = new GetUserToRuleResponse()
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    RoleId = item.RoleId
                };
                i++;
            }
            return Ok(createUserToRuleRequests);
        }


        /// <summary>
        /// Получение роли пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToRule
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли пользователя</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userToRuleService.GetById(id);
            var response = new GetUserToRuleResponse()
            {
                Id = result.Id,
                UserId = result.UserId,
                RoleId = result.RoleId
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новой роли пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToRule
        ///     {
        ///        "Id": 1,
        ///        "UserId": 1,
        ///        "RoleId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserToRuleRequest request)
        {
            var userToRule = request.Adapt<UserToRule>();
            await _userToRuleService.Create(userToRule);
            return Ok();
        }

        /// <summary>
        /// Изменить данные роли пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToRule
        ///     {
        ///        "Id": 1,
        ///        "UserId": 1,
        ///        "RoleId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли пользователя</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetUserToRuleResponse result)
        {
            var userToRule = result.Adapt<UserToRule>();
            await _userToRuleService.Update(userToRule);
            return Ok(result);
        }

        /// <summary>
        /// Удалить всех ролей пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /UserToRule
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetUserToRuleResponse request = new GetUserToRuleResponse();
            var userToRule = request.Adapt<UserToRule>();
            await _userToRuleService.Delete(id);
            return Ok();
        }
    }
}