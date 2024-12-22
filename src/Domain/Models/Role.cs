using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<UserToRule> UserToRules { get; set; } = new List<UserToRule>();
}
