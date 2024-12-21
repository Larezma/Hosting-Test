using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Group
{
    public int GroupsId { get; set; }

    public int OwnerGroups { get; set; }

    public string GroupsName { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateGroups { get; set; }

    public virtual GroupMember? GroupMember { get; set; }

    public virtual User OwnerGroupsNavigation { get; set; } = null!;
}