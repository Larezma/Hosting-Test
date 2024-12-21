﻿namespace BackendApi.Contract.TrainersSchedule
{
    public class GetTrainersScheduleResponse
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        public int TrainerId { get; set; }
        public string? TypeOfTraining { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public DateTime? CreateAt { get; set; }

    }
}