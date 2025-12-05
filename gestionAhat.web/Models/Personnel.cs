using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionsyndic.web.Models;

public  class Personnel
{
    [Key]
    public int Id { get; set; }

    public string? Specialite { get; set; }

    public string? Disponibilite { get; set; }

    public int? IdUtilisateur { get; set; }

    public  Utilisateur? IdUtilisateurNavigation { get; set; }
}
