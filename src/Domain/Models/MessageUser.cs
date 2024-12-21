using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class MessageUser
{
    public int MessageId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string MessageContent { get; set; } = null!;

    public DateTime? DateMessage { get; set; }

    public DateTime? DateUpMessage { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}