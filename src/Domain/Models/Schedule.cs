using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int TrainingId { get; set; }

    public int TrainerId { get; set; }

    public string TrainingType { get; set; } = null!;

    public string DayOfWeek { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual Trainer Trainer { get; set; } = null!;

    public virtual ICollection<TrainersSchedule> TrainersSchedules { get; set; } = new List<TrainersSchedule>();

    public virtual Training Training { get; set; } = null!;
}
