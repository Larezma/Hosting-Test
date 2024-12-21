using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Dialog
{
    public int DialogsId { get; set; }

    public string TextDialogs { get; set; } = null!;

    public DateTime? TimeCreate { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<UserToDialog> UserToDialogs { get; set; } = new List<UserToDialog>();
}