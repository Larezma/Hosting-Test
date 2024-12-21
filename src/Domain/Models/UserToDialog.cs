using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class UserToDialog
{
    public int Id { get; set; }

    public int DialogId { get; set; }

    public int UserId { get; set; }

    public DateTime? TimeCreate { get; set; }

    public virtual Dialog Dialog { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}