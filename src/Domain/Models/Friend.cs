using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Friend
{
    public int FriendId { get; set; }

    public int UserId1 { get; set; }

    public int UserId2 { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual User UserId1Navigation { get; set; } = null!;

    public virtual User UserId2Navigation { get; set; } = null!;
}
