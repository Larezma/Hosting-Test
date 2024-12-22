using Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Wrapper
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ITrainerRepository Trainer { get; }
        ITrainingRepository Training { get; }
        IScheduleRepository Schedule { get; }
        IRolesRepository Roles { get; }
        IPublicationRepository Publication { get; }
        IProductsRepository Products { get; }
        IPhotoUsersRepository PhotoUsers { get; }
        INutritionRepository Nutrition { get; }
        IMessageUsersRepository MessageUsers { get; }
        IGroupsRepository Group { get; }
        IGroupMembersRepository GroupMembers { get; }
        IFriendRepository Friend { get; }
        IDialogsRepository Dialogs { get; }
        ICommentsRepository Comments { get; }
        IAchievementsRepository Achievements { get; }
        IUserToRuleRepository UserToRule { get; }
        IUserToDialogRepository UserToDialog { get; }
        IUserToAchievementsRepository UserToAchievements { get; }
        IUserTrainingRepository UserTraining { get; }
        IUserNutritionRepository UserNutrition { get; }
        ITrainersScheduleRepository TrainersSchedule { get; }
        Task Save();
    }
}
