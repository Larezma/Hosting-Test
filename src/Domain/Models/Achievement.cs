using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Achievement
{
    public int AchievementsId { get; set; }

    public string AchievementsText { get; set; } = null!;

    public int AchievementsType { get; set; }

    public virtual ICollection<UserToAchievement> UserToAchievements { get; set; } = new List<UserToAchievement>();
}
