using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Training
{
    public int TrainingId { get; set; }

    public DateTime? TrainingDate { get; set; }

    public string DurationMinutes { get; set; } = null!;

    public decimal CaloriesBurned { get; set; }

    public string? Notes { get; set; }

    public string TrainingType { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<UserTraining> UserTrainings { get; set; } = new List<UserTraining>();
}