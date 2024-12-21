using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Product1 { get; set; } = null!;

    public decimal Calories { get; set; }

    public decimal ProteinPer { get; set; }

    public decimal FatPer { get; set; }

    public decimal CarbsPer { get; set; }

    public string VitaminsAndMinerals { get; set; } = null!;

    public string NutritionalValue { get; set; } = null!;

    public virtual ICollection<Nutrition> Nutritions { get; set; } = new List<Nutrition>();
}