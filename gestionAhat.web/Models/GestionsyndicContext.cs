using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace gestionsyndic.web.Models;

public partial class GestionsyndicContext : DbContext
{
    private readonly IConfiguration _configuration; // AJOUT MINIMAL

    public GestionsyndicContext()
    {
    }


    public GestionsyndicContext(DbContextOptions<GestionsyndicContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration; // AJOUT MINIMAL
    }

    public virtual DbSet<Coproprietaire> Coproprietaires { get; set; }
    public virtual DbSet<Immeuble> Immeubles { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<Paiement> Paiements { get; set; }
    public virtual DbSet<Personnel> Personnel { get; set; }
    public virtual DbSet<Syndic> Syndics { get; set; }
    public virtual DbSet<Technicien> Techniciens { get; set; }
    public virtual DbSet<Travaux> Travauxes { get; set; }
    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && _configuration != null)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SchoolContext")); // CORRIGÉ
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ⚠ Tout ton code reste IDENTIQUE
        modelBuilder.Entity<Coproprietaire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Copropri__3214EC0777D9A1F9");

            entity.ToTable("Coproprietaire");

            entity.HasIndex(e => e.IdUtilisateur, "UQ__Copropri__45A4C156F9DED18E").IsUnique();

            entity.Property(e => e.Adresse).HasMaxLength(150);
            entity.Property(e => e.Cin)
                .HasMaxLength(20)
                .HasColumnName("CIN");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Coproprietaire)
                .HasForeignKey<Coproprietaire>(d => d.IdUtilisateur)
                .HasConstraintName("FK__Coproprie__IdUti__44FF419A");

            entity.HasOne(d => d.Immeuble).WithMany(p => p.Coproprietaires)
                .HasForeignKey(d => d.ImmeubleId)
                .HasConstraintName("FK__Coproprie__Immeu__440B1D61");
        });

        modelBuilder.Entity<Immeuble>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Immeuble__3214EC07D8E56D2C");

            entity.ToTable("Immeuble");

            entity.Property(e => e.Adresse).HasMaxLength(150);
            entity.Property(e => e.Nom).HasMaxLength(100);

            entity.HasOne(d => d.IdSyndicNavigation).WithMany(p => p.Immeubles)
                .HasForeignKey(d => d.IdSyndic)
                .HasConstraintName("FK__Immeuble__IdSynd__403A8C7D");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Message__3214EC0711FBD88C");

            entity.ToTable("Message");

            entity.Property(e => e.DateEnvoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EstLu).HasDefaultValue(false);

            entity.HasOne(d => d.Destinataire).WithMany(p => p.MessageDestinataires)
                .HasForeignKey(d => d.DestinataireId)
                .HasConstraintName("FK__Message__Destina__5AEE82B9");

            entity.HasOne(d => d.Expediteur).WithMany(p => p.MessageExpediteurs)
                .HasForeignKey(d => d.ExpediteurId)
                .HasConstraintName("FK__Message__Expedit__59FA5E80");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07943CB455");

            entity.ToTable("Notification");

            entity.Property(e => e.DateEnvoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Lu).HasDefaultValue(false);

            entity.HasOne(d => d.Destinataire).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.DestinataireId)
                .HasConstraintName("FK__Notificat__Desti__5FB337D6");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paiement__3214EC07BCAD35C8");

            entity.ToTable("Paiement");

            entity.Property(e => e.DatePaiement).HasColumnType("datetime");
            entity.Property(e => e.ModePaiement).HasMaxLength(50);
            entity.Property(e => e.Montant).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TypePaiement).HasMaxLength(50);

            entity.HasOne(d => d.IdCoproprietaireNavigation).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.IdCoproprietaire)
                .HasConstraintName("FK__Paiement__IdCopr__48CFD27E");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personne__3214EC074F894CCB");

            entity.HasIndex(e => e.IdUtilisateur, "UQ__Personne__45A4C15635382EA7").IsUnique();

            entity.Property(e => e.Disponibilite).HasMaxLength(100);
            entity.Property(e => e.Specialite).HasMaxLength(50);

            entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Personnel)
                .HasForeignKey<Personnel>(d => d.IdUtilisateur)
                .HasConstraintName("FK__Personnel__IdUti__5535A963");
        });

        modelBuilder.Entity<Syndic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Syndic__3214EC07A7CE3DB0");

            entity.ToTable("Syndic");

            entity.HasIndex(e => e.IdUtilisateur, "UQ__Syndic__45A4C156E34BF2C6").IsUnique();

            entity.Property(e => e.AdresseBureau).HasMaxLength(150);
            entity.Property(e => e.Cin).HasMaxLength(20).HasColumnName("CIN");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Syndic)
                .HasForeignKey<Syndic>(d => d.IdUtilisateur)
                .HasConstraintName("FK__Syndic__IdUtilis__3D5E1FD2");
        });

        modelBuilder.Entity<Technicien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Technici__3214EC07418BAC17");

            entity.ToTable("Technicien");

            entity.HasIndex(e => e.IdUtilisateur, "UQ__Technici__45A4C156DCB155E6").IsUnique();

            entity.Property(e => e.Entreprise).HasMaxLength(100);
            entity.Property(e => e.Specialite).HasMaxLength(100);

            entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Technicien)
                .HasForeignKey<Technicien>(d => d.IdUtilisateur)
                .HasConstraintName("FK__Technicie__IdUti__5070F446");
        });

        modelBuilder.Entity<Travaux>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Travaux__3214EC07F18B0702");

            entity.ToTable("Travaux");

            entity.Property(e => e.Cout).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DateDebut).HasColumnType("datetime");
            entity.Property(e => e.DateFin).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Statut).HasMaxLength(50);

            entity.HasOne(d => d.IdImmeubleNavigation).WithMany(p => p.Travauxes)
                .HasForeignKey(d => d.IdImmeuble)
                .HasConstraintName("FK__Travaux__IdImmeu__4BAC3F29");

            entity.HasOne(d => d.IdPaiementNavigation).WithMany(p => p.Travauxes)
                .HasForeignKey(d => d.IdPaiement)
                .HasConstraintName("FK__Travaux__IdPaiem__4CA06362");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Utilisat__3214EC07A948A60B");

            entity.ToTable("Utilisateur");

            entity.HasIndex(e => e.Email, "UQ__Utilisat__A9D1053459EB10EB").IsUnique();

            entity.Property(e => e.DateInscription)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EstActif).HasDefaultValue(true);
            entity.Property(e => e.MotDePasse).HasMaxLength(255);
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.Prenom).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
