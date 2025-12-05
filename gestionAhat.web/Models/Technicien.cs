using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionsyndic.web.Models;

public partial class Technicien
{
    [Key]
    public int Id { get; set; }

    public string? Specialite { get; set; }

    public string? Entreprise { get; set; }

    public int? IdUtilisateur { get; set; }

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }
}
