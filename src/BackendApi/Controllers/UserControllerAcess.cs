using Azure.Core;
using BackendApi.Contract.Friend;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using DataAccess.Repositories;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllerAcess : Controller
    {
        private IUserService _userService;
        public UserControllerAcess(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            GetUserResponse[] createUserRequests = new GetUserResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createUserRequests[i] = new GetUserResponse()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Email = item.Email,
                    Password = item.Password,
                    RoleUser = item.RoleUser,
                    UserImg = item.UserImg,
                    CreateAt = item.CreateAt,
                    UpdateAt = item.UpdateAt,
                    PhoneNumber = item.PhoneNumber,
                    AboutMe = item.AboutMe,
                };
                i++;
            }
            return Ok(createUserRequests);
        }


        /// <summary>
        /// Получение пользователя по уникальному-идентификатору(id).
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
        /// <param name="model">Пользователь</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            var response = new GetUserResponse()
            {
                UserId = result.UserId,
                Email = result.Email,
                Password = result.Password,
                RoleUser = result.RoleUser,
                UserImg = result.UserImg,
                CreateAt = result.CreateAt,
                UpdateAt = result.UpdateAt,
                PhoneNumber = result.PhoneNumber,
                AboutMe = result.AboutMe,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Users
        ///     {
        ///        "userName": "GG",
        ///        "email": "ssd@gmail.com",
        ///        "password": "12345",
        ///        "roleUser": 0,
        ///        "userImg": null,
        ///        "phoneNumber": "123444444",
        ///        "aboutMe": null,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var userUser = request.Adapt<User>();
            await _userService.Create(userUser);
            return Ok();
        }

        /// <summary>
        /// Изменить данные пользователя
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
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetUserResponse result)
        {
            var user = result.Adapt<User>();
            await _userService.Update(user);
            return Ok(result);
        }

        /// <summary>
        /// Удалить пользователя по уникальному-идентификатору(id).
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
        /// <param name="model">Пользователь</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetUserResponse request = new GetUserResponse();
            var userUser = request.Adapt<User>();
            await _userService.Delete(id);
            return Ok();
        }
    }
}