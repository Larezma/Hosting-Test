namespace BackendApi.Contract.Achievements
{
    public class GetAchievementsResponse
    {
        public int AchievementsId { get; set; }

        public string AchievementsText { get; set; } = null!;

        public int AchievementsType { get; set; }
    }
}