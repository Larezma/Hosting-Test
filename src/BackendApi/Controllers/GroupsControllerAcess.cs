using BackendApi.Contract.Groups;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsControllerAcess : Controller
    {
        private IGroupsService _groupsService;
        public GroupsControllerAcess(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        /// <summary>
        /// Получение всех групп
        /// </summary>
        /// <param name="model">Группы</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _groupsService.GetAll();
            GetGroupsResponse[] createGroupRequests = new GetGroupsResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createGroupRequests[i] = new GetGroupsResponse()
                {
                    GroupsId = item.GroupsId,
                    OwnerGroups = item.OwnerGroups,
                    GroupsName = item.GroupsName,
                    CreateDate = item.CreateDate,
                    UpdateGroups = item.UpdateGroups,
                };
                i++;
            }
            return Ok(createGroupRequests);
        }


        /// <summary>
        /// Получение группу по уникальному-идентификатору(id).
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
        /// <param name="model">Группы</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _groupsService.GetById(id);
            var response = new GetGroupsResponse()
            {
                GroupsId = result.GroupsId,
                OwnerGroups = result.OwnerGroups,
                GroupsName = result.GroupsName,
                CreateDate = result.CreateDate,
                UpdateGroups = result.UpdateGroups,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новую группу
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Group
        ///     {
        ///        "GroupsId": 1,
        ///        "userName": "GG",
        ///        "OwnerGroups": "ssd@gmail.com",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Группы</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateGroupsRequest request)
        {
            var group = request.Adapt<Group>();
            await _groupsService.Create(group);
            return Ok();
        }

        /// <summary>
        /// Изменить данные группы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Group
        ///     {
        ///        "GroupsId": 1,
        ///        "userName": "GG",
        ///        "OwnerGroups": "ssd@gmail.com",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Группы</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetGroupsResponse result)
        {
            var group = result.Adapt<Group>();
            await _groupsService.Update(group);
            return Ok(result);
        }

        /// <summary>
        /// Удалить группу по уникальному-идентификатору(id).
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
        /// <param name="model">Группы</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetGroupsResponse request = new GetGroupsResponse();
            var group = request.Adapt<Group>();
            await _groupsService.Delete(id);
            return Ok();
        }
    }
}