using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class PhotoUser
{
    public int PhotoId { get; set; }

    public int UserId { get; set; }

    public string PhotoLink { get; set; } = null!;

    public DateTime? UploadPhoto { get; set; }

    public virtual User User { get; set; } = null!;
}
