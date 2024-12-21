using BackendApi.Contract.Roles;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesControllerAcess : Controller
    {
        private IRolesService _rolesService;
        public RolesControllerAcess(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }


        /// <summary>
        /// Получение всех ролей
        /// </summary>
        /// <param name="model">Роли</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rolesService.GetAll();
            GetRolesResponse[] createRolesRequests = new GetRolesResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createRolesRequests[i] = new GetRolesResponse()
                {
                    Id = item.Id,
                    Roles = item.Role1,
                };
                i++;
            }
            return Ok(createRolesRequests);
        }


        /// <summary>
        /// Получение роли по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Role
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _rolesService.GetById(id);
            var response = new GetRolesResponse()
            {
                Id = result.Id,
                Roles = result.Role1,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новую роль
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Role
        ///     {
        ///        "Id": 1,
        ///        "Roles": "User,Admin,Trainer ////",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateRolesRequest request)
        {
            var role = request.Adapt<Role>();
            await _rolesService.Create(role);
            return Ok();
        }

        /// <summary>
        /// Изменить данные роли
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Role
        ///     {
        ///        "Id": 1,
        ///        "Roles": "User,Admin,Trainer ////",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetRolesResponse result)
        {
            var role = result.Adapt<Role>();
            await _rolesService.Update(role);
            return Ok(result);
        }

        /// <summary>
        /// Удалить роль по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Role
        ///     {
        ///        "Id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Роли</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetRolesResponse request = new GetRolesResponse();
            var role = request.Adapt<Role>();
            await _rolesService.Delete(id);
            return Ok();
        }
    }
}