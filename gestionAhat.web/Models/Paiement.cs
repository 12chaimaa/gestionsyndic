using System;
using System.Collections.Generic;

namespace gestionsyndic.web.Models;

public partial class Paiement
{
    public int Id { get; set; }

    public decimal? Montant { get; set; }

    public DateTime? DatePaiement { get; set; }

    public string? ModePaiement { get; set; }

    public string? TypePaiement { get; set; }

    public int? IdCoproprietaire { get; set; }

    public virtual Coproprietaire? IdCoproprietaireNavigation { get; set; }

    public virtual ICollection<Travaux> Travauxes { get; set; } = new List<Travaux>();
}
