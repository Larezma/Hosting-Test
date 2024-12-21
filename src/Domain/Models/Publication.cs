using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Publication
{
    public int PublicationsId { get; set; }

    public int UsersId { get; set; }

    public string PublicationText { get; set; } = null!;

    public DateTime? PublicationDate { get; set; }

    public string? PublicationsImage { get; set; }

    public virtual User Users { get; set; } = null!;
}