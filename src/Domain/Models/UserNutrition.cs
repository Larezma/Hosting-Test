using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserNutrition
{
    public int UserNutritionId { get; set; }

    public int UserId { get; set; }

    public int NutritionId { get; set; }

    public DateTime DateOfAdmission { get; set; }

    public DateTime AppointmentTime { get; set; }

    public string NutritionType { get; set; } = null!;

    public string Report { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual Nutrition Nutrition { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
