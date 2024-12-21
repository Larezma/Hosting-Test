using DataAccess.Repositories;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Wrapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private VitalityMasteryTestContext _context;

        private IUserRepository _user;
        private ITrainerRepository _trainer;
        private IScheduleRepository _schedule;
        private ITrainingRepository _training;
        private IRolesRepository _roles;
        private IPublicationRepository _publication;
        private IProductsRepository _product;
        private IPhotoUsersRepository _photo_user;
        private INutritionRepository _nutrition;
        private IMessageUsersRepository _message_user;
        private IGroupsRepository _group;
        private IGroupMembersRepository _group_members;
        private IFriendRepository _friend;
        private IDialogsRepository _dialogs;
        private ICommentsRepository _comments;
        private IAchievementsRepository _achievements;
        private IUserToRuleRepository _user_to_rule;
        private IUserToDialogRepository _user_to_dialogs;
        private IUserToAchievementsRepository _user_to_achievements;
        private IUserTrainingRepository _user_training;
        private IUserNutritionRepository _user_nutrition;
        private ITrainersScheduleRepository _trainers_Schedule;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public ITrainerRepository Trainer
        {
            get
            {
                if (_trainer == null)
                {
                    _trainer = new TrainerRepository(_context);
                }
                return _trainer;
            }
        }

        public IScheduleRepository Schedule
        {
            get
            {
                if (_schedule == null)
                {
                    _schedule = new ScheduleRepository(_context);
                }
                return _schedule;
            }
        }

        public ITrainingRepository Training
        {
            get
            {
                if (_training == null)
                {
                    _training = new TrainingRepository(_context);
                }
                return _training;
            }
        }

        public IRolesRepository Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolesRepository(_context);
                }
                return _roles;
            }
        }

        public IPublicationRepository Publication
        {
            get
            {
                if (_publication == null)
                {
                    _publication = new PublicationRepository(_context);
                }
                return _publication;
            }
        }

        public IProductsRepository Products
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductsRepository(_context);
                }
                return _product;
            }
        }

        public IPhotoUsersRepository PhotoUsers
        {
            get
            {
                if (_photo_user == null)
                {
                    _photo_user = new PhotoUsersRepository(_context);
                }
                return _photo_user;
            }
        }

        public INutritionRepository Nutrition
        {
            get
            {
                if (_nutrition == null)
                {
                    _nutrition = new NutritionRepository(_context);
                }
                return _nutrition;
            }
        }

        public IMessageUsersRepository MessageUsers
        {
            get
            {
                if (_message_user == null)
                {
                    _message_user = new MessageUsersRepository(_context);
                }
                return _message_user;
            }
        }

        public IGroupsRepository Group
        {
            get
            {
                if (_group == null)
                {
                    _group = new GroupsRepository(_context);
                }
                return _group;
            }
        }

        public IGroupMembersRepository GroupMembers
        {
            get
            {
                if (_group_members == null)
                {
                    _group_members = new GroupMembersRepository(_context);
                }
                return _group_members;
            }
        }

        public IFriendRepository Friend
        {
            get
            {
                if (_friend == null)
                {
                    _friend = new FriendRepository(_context);
                }
                return _friend;
            }
        }

        public IDialogsRepository Dialogs
        {
            get
            {
                if (_dialogs == null)
                {
                    _dialogs = new DialogsRepository(_context);
                }
                return _dialogs;
            }
        }

        public ICommentsRepository Comments
        {
            get
            {
                if (_comments == null)
                {
                    _comments = new CommentsRepository(_context);
                }
                return _comments;
            }
        }

        public IAchievementsRepository Achievements
        {
            get
            {
                if (_achievements == null)
                {
                    _achievements = new AchievementsRepository(_context);
                }
                return _achievements;
            }
        }

        public IUserToRuleRepository UserToRule
        {
            get
            {
                if (_user_to_rule == null)
                {
                    _user_to_rule = new UserToRuleRepository(_context);
                }
                return _user_to_rule;
            }
        }

        public IUserToDialogRepository UserToDialog
        {
            get
            {
                if (_user_to_dialogs == null)
                {
                    _user_to_dialogs = new UserToDialogsRepository(_context);
                }
                return _user_to_dialogs;
            }
        }

        public IUserToAchievementsRepository UserToAchievements
        {
            get
            {
                if (_user_to_achievements == null)
                {
                    _user_to_achievements = new UserToAchievementsRepository(_context);
                }
                return _user_to_achievements;
            }
        }

        public IUserTrainingRepository UserTraining
        {
            get
            {
                if (_user_training == null)
                {
                    _user_training = new UserTrainingRepository(_context);
                }
                return _user_training;
            }
        }

        public IUserNutritionRepository UserNutrition
        {
            get
            {
                if (_user_nutrition == null)
                {
                    _user_nutrition = new UserNutritionRepository(_context);
                }
                return _user_nutrition;
            }
        }

        public ITrainersScheduleRepository TrainersSchedule
        {
            get
            {
                if (_trainers_Schedule == null)
                {
                    _trainers_Schedule = new TrainersScheduleRepository(_context);
                }
                return _trainers_Schedule;
            }
        }

        public RepositoryWrapper(VitalityMasteryTestContext vitalityContext)
        {
            _context = vitalityContext;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}