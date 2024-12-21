using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Nutrition
{
    public int NutritionId { get; set; }

    public int Product { get; set; }

    public string MeanType { get; set; } = null!;

    public string MeanDeacription { get; set; } = null!;

    public DateTime DateNutrition { get; set; }

    public virtual Product ProductNavigation { get; set; } = null!;

    public virtual ICollection<UserNutrition> UserNutritions { get; set; } = new List<UserNutrition>();
}