using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionsyndic.web.Models;

public partial class Coproprietaire
{
    [Key]
    public int Id { get; set; }

    public string? Cin { get; set; }

    public string? Adresse { get; set; }

    public int? ImmeubleId { get; set; }

    public int? IdUtilisateur { get; set; }

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }

    public virtual Immeuble? Immeuble { get; set; }

    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
