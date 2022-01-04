using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VestigeSalon
{
    public partial class VestigeEntitiesSalon : DbContext
    {
        public VestigeEntitiesSalon()
            : base("name=VestigeEntitiesSalon")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Coafura> Coafuras { get; set; }
        public virtual DbSet<Hairstylist> Hairstylists { get; set; }
        public virtual DbSet<Programare> Programares { get; set; }
        public virtual DbSet<Vopsit> Vopsits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Telefon)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Programares)
                .WithOptional(e => e.Client)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Coafura>()
                .Property(e => e.Pret)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Hairstylist>()
                .HasMany(e => e.Programares)
                .WithOptional(e => e.Hairstylist)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Vopsit>()
                .Property(e => e.PretV)
                .HasPrecision(19, 4);
        }
    }
}
