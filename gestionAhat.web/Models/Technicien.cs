using System;
using System.Collections.Generic;

namespace gestionsyndic.web.Models;

public partial class Technicien
{
    public int Id { get; set; }

    public string? Specialite { get; set; }

    public string? Entreprise { get; set; }

    public int? IdUtilisateur { get; set; }

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }
}
