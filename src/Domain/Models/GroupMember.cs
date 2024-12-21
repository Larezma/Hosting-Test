using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class GroupMember
{
    public int GroupsId { get; set; }

    public int UserId { get; set; }

    public DateTime? JoinDate { get; set; }

    public virtual Group Groups { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}