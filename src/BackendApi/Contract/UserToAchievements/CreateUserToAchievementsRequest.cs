namespace BackendApi.Contract.UserToAchievements
{
    public class CreateUserToAchievementsRequest
    {
        public int UserId { get; set; }

        public int AchievementsId { get; set; }

        public DateTime? GetDateAchievements { get; set; }
    }
}