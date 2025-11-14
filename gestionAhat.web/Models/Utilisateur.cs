using System;
using System.Collections.Generic;

namespace gestionsyndic.web.Models;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Role { get; set; }

    public DateTime? DateInscription { get; set; }

    public bool? EstActif { get; set; }

    public virtual Coproprietaire? Coproprietaire { get; set; }

    public virtual ICollection<Message> MessageDestinataires { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageExpediteurs { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Personnel? Personnel { get; set; }

    public virtual Syndic? Syndic { get; set; }

    public virtual Technicien? Technicien { get; set; }
}
