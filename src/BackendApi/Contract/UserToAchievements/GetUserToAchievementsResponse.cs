namespace BackendApi.Contract.UserToAchievements
{
    public class GetUserToAchievementsResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AchievementsId { get; set; }

        public DateTime? GetDateAchievements { get; set; }
    }
}