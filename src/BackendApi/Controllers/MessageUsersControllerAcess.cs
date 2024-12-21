using BackendApi.Contract.MessageUsers;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageUsersControllerAcess : Controller
    {
        private IMessageUsersService _messageUsersService;
        public MessageUsersControllerAcess(IMessageUsersService messageUsersService)
        {
            _messageUsersService = messageUsersService;
        }

        /// <summary>
        /// Получение всех сообщений
        /// </summary>
        /// <param name="model">Сообщений</param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _messageUsersService.GetAll();
            GetMessageUsersResponse[] createMessageUsersRequests = new GetMessageUsersResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createMessageUsersRequests[i] = new GetMessageUsersResponse()
                {
                    MessageId = item.MessageId,
                    SenderId = item.SenderId,
                    ReceiverId = item.ReceiverId,
                    MessageContent = item.MessageContent,
                    DateMessage = item.DateMessage,
                    DateUpMessage = item.DateUpMessage,
                };
                i++;
            }
            return Ok(createMessageUsersRequests);
        }


        /// <summary>
        /// Получение сообщения по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /MessageUsers
        ///     {
        ///        "MessageId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Сообщений</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _messageUsersService.GetById(id);
            var response = new GetMessageUsersResponse()
            {
                MessageId = result.MessageId,
                SenderId = result.SenderId,
                ReceiverId = result.ReceiverId,
                MessageContent = result.MessageContent,
                DateMessage = result.DateMessage,
                DateUpMessage = result.DateUpMessage,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать новое сообщение
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /MessageUsers
        ///     {
        ///        "MessageId": 1,
        ///        "SenderId": "1",
        ///        "ReceiverId": "1",
        ///        "MessageContent": "sssss",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Сообщений</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateMessageUsersRequest request)
        {
            var messageUser = request.Adapt<MessageUser>();
            await _messageUsersService.Create(messageUser);
            return Ok();
        }

        /// <summary>
        /// Изменить данные сообщения
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /MessageUsers
        ///     {
        ///        "MessageId": 1,
        ///        "SenderId": "1",
        ///        "ReceiverId": "1",
        ///        "MessageContent": "sssss",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Сообщений</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetMessageUsersResponse result)
        {
            var messageUser = result.Adapt<MessageUser>();
            await _messageUsersService.Update(messageUser);
            return Ok(result);
        }

        /// <summary>
        /// Удалить сообщение по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /MessageUsers
        ///     {
        ///        "MessageId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Сообщений</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetMessageUsersResponse request = new GetMessageUsersResponse();
            var messageUser = request.Adapt<MessageUser>();
            await _messageUsersService.Delete(id);
            return Ok();
        }
    }
}