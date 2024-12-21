using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserToAchievement
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AchievementsId { get; set; }

    public DateTime? GetDateAchievements { get; set; }

    public virtual Achievement Achievements { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}