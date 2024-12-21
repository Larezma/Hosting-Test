using System;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Service
{
    public interface IUserToAchievementService
    {
        Task<List<UserToAchievement>> GetAll();
        Task<UserToAchievement> GetById(int id);
        Task Create(UserToAchievement model);
        Task Update(UserToAchievement model);
        Task Delete(int id);
    }
}
