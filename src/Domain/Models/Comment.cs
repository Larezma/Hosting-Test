using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Comment
{
    public int CommentsId { get; set; }

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public int ItemType { get; set; }

    public string CommentsText { get; set; } = null!;

    public DateTime? CommentsDate { get; set; }

    public virtual User User { get; set; } = null!;
}