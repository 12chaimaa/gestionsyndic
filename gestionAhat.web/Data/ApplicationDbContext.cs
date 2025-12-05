using Microsoft.EntityFrameworkCore;
using gestionsyndic.web.Models;

namespace gestionsyndic.web.newfolder
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Syndic> Syndics { get; set; }
        public DbSet<Immeuble> Immeubles { get; set; }
        public DbSet<Coproprietaire> Coproprietaires { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Travaux> Travauxes { get; set; }
        public DbSet<Technicien> Techniciens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation : Utilisateur → Messages envoyés
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Expediteur)
                .WithMany(u => u.MessageExpediteurs)
                .HasForeignKey(m => m.ExpediteurId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation : Utilisateur → Messages reçus
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Destinataire)
                .WithMany(u => u.MessageDestinataires)
                .HasForeignKey(m => m.DestinataireId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
