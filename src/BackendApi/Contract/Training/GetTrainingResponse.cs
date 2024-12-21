namespace BackendApi.Contract.Training
{
    public class GetTrainingResponse
    {
        public int TrainingId { get; set; }

        public DateTime? TrainingDate { get; set; }

        public string DurationMinutes { get; set; } = null!;

        public decimal CaloriesBurned { get; set; }

        public string? Notes { get; set; }

        public string TrainingType { get; set; } = null!;
    }
}