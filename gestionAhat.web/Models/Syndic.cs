using System;
using System.Collections.Generic;

namespace gestionsyndic.web.Models;

public partial class Syndic
{
    public int Id { get; set; }

    public string? AdresseBureau { get; set; }

    public string? Cin { get; set; }

    public int? IdUtilisateur { get; set; }

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }

    public virtual ICollection<Immeuble> Immeubles { get; set; } = new List<Immeuble>();
}
