using BackendApi.Contract.GroupMembers;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersControllerAcess : Controller
    {
        private IGroupMembersService _groupMembersService;
        public GroupMembersControllerAcess(IGroupMembersService groupMembersService)
        {
            _groupMembersService = groupMembersService;
        }

        /// <summary>
        /// Получение всех пользователей группы
        /// </summary>
        /// <param name="model">Пользователи группы</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _groupMembersService.GetAll();
            GetGroupMembersResponse[] createUserRequests = new GetGroupMembersResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createUserRequests[i] = new GetGroupMembersResponse()
                {
                    GroupsId = item.GroupsId,
                    UserId = item.UserId,
                    JoinDate = item.JoinDate,
                };
                i++;
            }
            return Ok(createUserRequests);
        }


        /// <summary>
        /// Получение пользователей группы по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /GroupMembers
        ///     {
        ///        "GroupsId": 1,
        ///        "UserId": 1,
        ///        "JoinDate": "Автозаполнение",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователи группы</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _groupMembersService.GetById(id);
            var response = new GetGroupMembersResponse()
            {
                GroupsId = result.GroupsId,
                UserId = result.UserId,
                JoinDate = result.JoinDate,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового пользователей группы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /GroupMembers
        ///     {
        ///        "GroupsId": 1,
        ///        "UserId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователи группы</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var groupMember = request.Adapt<GroupMember>();
            await _groupMembersService.Create(groupMember);
            return Ok();
        }

        /// <summary>
        /// Изменить данные пользователей группы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /GroupMembers
        ///     {
        ///        "GroupsId": 1,
        ///        "UserId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователи группы</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetGroupMembersResponse result)
        {
            var groupMember = result.Adapt<GroupMember>();
            await _groupMembersService.Update(groupMember);
            return Ok(result);
        }

        /// <summary>
        /// Удалить пользователей группы по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Group
        ///     {
        ///        "GroupsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователи группы</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetGroupMembersResponse request = new GetGroupMembersResponse();
            var groupMember = request.Adapt<GroupMember>();
            await _groupMembersService.Delete(id);
            return Ok();
        }
    }
}