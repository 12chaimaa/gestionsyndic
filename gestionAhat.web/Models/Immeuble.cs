using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionsyndic.web.Models;

public partial class Immeuble
{
    [Key]
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Adresse { get; set; }

    public int? NombreEtages { get; set; }

    public int? IdSyndic { get; set; }

    public virtual ICollection<Coproprietaire> Coproprietaires { get; set; } = new List<Coproprietaire>();

    public virtual Syndic? IdSyndicNavigation { get; set; }

    public virtual ICollection<Travaux> Travauxes { get; set; } = new List<Travaux>();
}
