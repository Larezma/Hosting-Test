using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IPhotoUsersService
    {
        Task<List<PhotoUser>> GetAll();
        Task<PhotoUser> GetById(int id);
        Task Create(PhotoUser model);
        Task Update(PhotoUser model);
        Task Delete(int id);
    }
}