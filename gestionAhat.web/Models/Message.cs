using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionsyndic.web.Models;

public partial class Message
{
    [Key]
    public int Id { get; set; }

    public string? Contenu { get; set; }

    public DateTime? DateEnvoi { get; set; }

    public bool? EstLu { get; set; }

    public int? ExpediteurId { get; set; }

    public int? DestinataireId { get; set; }

    public virtual Utilisateur? Destinataire { get; set; }

    public virtual Utilisateur? Expediteur { get; set; }
}
