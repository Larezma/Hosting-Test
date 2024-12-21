using BackendApi.Contract.Publication;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationControllerAcess : Controller
    {
        private IPublicationService _publicationService;
        public PublicationControllerAcess(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        /// <summary>
        /// Получение всех публикации
        /// </summary>
        /// <param name="model">Публикации</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _publicationService.GetAll();
            GetPublicationResponse[] createPublicationRequests = new GetPublicationResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createPublicationRequests[i] = new GetPublicationResponse()
                {
                    PublicationsId = item.PublicationsId,
                    UsersId = item.UsersId,
                    PublicationText = item.PublicationText,
                    PublicationDate = item.PublicationDate,
                    PublicationsImage = item.PublicationsImage,
                };
                i++;
            }
            return Ok(createPublicationRequests);
        }


        /// <summary>
        /// Получение публикаций по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Publication
        ///     {
        ///        "PublicationsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Публикации</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _publicationService.GetById(id);
            var response = new GetPublicationResponse()
            {
                PublicationsId = result.PublicationsId,
                UsersId = result.UsersId,
                PublicationText = result.PublicationText,
                PublicationDate = result.PublicationDate,
                PublicationsImage = result.PublicationsImage,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового публикаций
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Publication
        ///     {
        ///        "PublicationsId": 1,
        ///        "UsersId": 1,
        ///        "PublicationText": "sdsdsdsd",
        ///        "PublicationsImage": "sdsdssds",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Публикации</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreatePublicationRequest request)
        {
            var publication = request.Adapt<Publication>();
            await _publicationService.Create(publication);
            return Ok();
        }

        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Publication
        ///     {
        ///        "PublicationsId": 1,
        ///        "UsersId": 1,
        ///        "PublicationText": "sdsdsdsd",
        ///        "PublicationsImage": "sdsdssds",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Публикации</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetPublicationResponse result)
        {
            var publication = result.Adapt<Publication>();
            await _publicationService.Update(publication);
            return Ok(result);
        }

        /// <summary>
        /// Удалить пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Publication
        ///     {
        ///        "PublicationsId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Публикации</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetPublicationResponse request = new GetPublicationResponse();
            var publication = request.Adapt<Publication>();
            await _publicationService.Delete(id);
            return Ok();
        }
    }
}