using BackendApi.Contract.PhotoUsers;
using BackendApi.Contract.Users;
using BusinessLogic.Services;
using Domain.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.ControllersDomain
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoUsersControllerAcess : Controller
    {
        private IPhotoUsersService _photoUsersService;
        public PhotoUsersControllerAcess(IPhotoUsersService photoUsersService)
        {
            _photoUsersService = photoUsersService;
        }

        /// <summary>
        /// Получение всех фото пользователя
        /// </summary>
        /// <param name="model">Фото пользователя</param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _photoUsersService.GetAll();
            GetPhotoUsersResponse[] createPhotoUserRequests = new GetPhotoUsersResponse[result.Count];
            int i = 0;
            foreach (var item in result)
            {
                createPhotoUserRequests[i] = new GetPhotoUsersResponse()
                {
                    PhotoId = item.PhotoId,
                    UserId = item.UserId,
                    PhotoLink = item.PhotoLink,
                    UploadPhoto = item.UploadPhoto,
                };
                i++;
            }
            return Ok(createPhotoUserRequests);
        }


        /// <summary>
        /// Получение фото пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /PhotoUser
        ///     {
        ///        "PhotoId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Фото пользователя</param>
        /// <returns></returns>


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _photoUsersService.GetById(id);
            var response = new GetPhotoUsersResponse()
            {
                PhotoId = result.PhotoId,
                UserId = result.UserId,
                PhotoLink = result.PhotoLink,
                UploadPhoto = result.UploadPhoto,
            };
            return Ok(response);
        }
        /// <summary>
        /// Создать нового фото пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /PhotoUser
        ///     {
        ///        "PhotoId": 1,
        ///        "UserId": "1",
        ///        "PhotoLink": "/descktop/ip.png....",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Фото пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreatePhotoUsersRequest request)
        {
            var photoUser = request.Adapt<PhotoUser>();
            await _photoUsersService.Create(photoUser);
            return Ok();
        }

        /// <summary>
        /// Изменить данные фото пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /PhotoUser
        ///     {
        ///        "PhotoId": 1,
        ///        "UserId": "1",
        ///        "PhotoLink": "/descktop/ip.png....",
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Фото пользователя</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPut]
        public async Task<IActionResult> Update(GetPhotoUsersResponse result)
        {
            var photoUser = result.Adapt<PhotoUser>();
            await _photoUsersService.Update(photoUser);
            return Ok(result);
        }

        /// <summary>
        /// Удалить пользователя по уникальному-идентификатору(id).
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /PhotoUser
        ///     {
        ///        "PhotoId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Фото пользователя</param>
        /// <returns></returns>

        // POST api/<UsersController>

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            GetPhotoUsersResponse request = new GetPhotoUsersResponse();
            var photoUser = request.Adapt<PhotoUser>();
            await _photoUsersService.Delete(id);
            return Ok();
        }
    }
}