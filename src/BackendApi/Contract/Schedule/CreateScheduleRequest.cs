namespace BackendApi.Contract.Schedule
{
    public class CreateScheduleRequest
    {
        public int TrainingId { get; set; }

        public int TrainerId { get; set; }

        public string TrainingType { get; set; } = null!;

        public string DayOfWeek { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}