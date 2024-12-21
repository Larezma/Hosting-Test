namespace BackendApi.Contract.Achievements
{
    public class CreateAchievementsRequest
    {
        public string AchievementsText { get; set; } = null!;

        public int AchievementsType { get; set; }
    }
}