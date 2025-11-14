using System;
using System.Collections.Generic;

namespace gestionsyndic.web.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string? Message { get; set; }

    public DateTime? DateEnvoi { get; set; }

    public bool? Lu { get; set; }

    public int? DestinataireId { get; set; }

    public virtual Utilisateur? Destinataire { get; set; }
}
