using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<TrainersSchedule> TrainersSchedules { get; set; } = new List<TrainersSchedule>();

    public virtual ICollection<UserTraining> UserTrainings { get; set; } = new List<UserTraining>();
}
