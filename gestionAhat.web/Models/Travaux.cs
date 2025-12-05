using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionsyndic.web.Models;

public partial class Travaux
{
    [Key]
    public int Id { get; set; }

    public string? Description { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public string? Statut { get; set; }

    public decimal? Cout { get; set; }

    public int? IdImmeuble { get; set; }

    public int? IdPaiement { get; set; }

    public virtual Immeuble? IdImmeubleNavigation { get; set; }

    public virtual Paiement? IdPaiementNavigation { get; set; }
   
}
